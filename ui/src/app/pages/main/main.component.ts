import { CommonModule } from '@angular/common'
import { HttpClient, HttpClientModule } from '@angular/common/http'
import { Component } from '@angular/core'
import { MatIconModule } from '@angular/material/icon'
import { MatTableModule } from '@angular/material/table'
import { MatTabsModule } from '@angular/material/tabs'
import { MatCardModule } from '@angular/material/card'
import { MatListModule } from '@angular/material/list'
import { MatBadgeModule } from '@angular/material/badge'
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { FormsModule } from '@angular/forms'
import { MatButtonModule } from '@angular/material/button'
import { MatInputModule } from '@angular/material/input'

@Component({
    selector: 'app-main',
    standalone: true,
    imports: [
        CommonModule,
        FormsModule,
        HttpClientModule,
        MatTableModule,
        MatTabsModule,
        MatIconModule,
        MatCardModule,
        MatListModule,
        MatBadgeModule,
        MatSlideToggleModule,
        MatButtonModule,
        MatInputModule
    ],
    templateUrl: './main.component.html',
    styleUrls: ['./main.component.scss'],
})
export class MainComponent {

    members: Member[]
    membersColumns: string[]

    teams: Team[]

    showTiers: boolean
    useMultiplier: boolean
    name: string

    constructor(
        private httpClient: HttpClient
    ) {
        this.name = ''
        this.showTiers = true
        this.useMultiplier = false
        this.members = []
        this.membersColumns = ['name', 'score', 'faceit', 'mm']
        this.teams = []
        this.loadSourcesAsync().then(_ => this.randomizeTeams())
    }

    async loadSourcesAsync(): Promise<void> {
        return new Promise(async resolve => {

            this.members = (await this.httpClient.get<Member[]>('assets/members.json').toPromise())
                .sort((a, b) => a.groupLeader && !b.groupLeader ? -1 : 1)
                .sort((a, b) =>
                    b.score - a.score ||
                    b.faceit.level - a.faceit.level ||
                    b.mm.level - a.mm.level)

            this.teams = await this.httpClient.get<Team[]>('assets/teams.json').toPromise()

            return resolve()
        })
    }

    getMember(id: number): Member {
        return this.members.find(x => x.id === id)!
    }

    randomizeTeams(): void {
        this.teams.forEach(t => t.members = [])
        const members = this.members.filter(x => !x.groupLeader)
        this.randomizePartial(members.filter(x => x.score > 14))
        this.randomizePartial(members.filter(x => x.score < 15))
        this.calculateTeamDRS()
    }

    randomizePartial(members: Member[]) {
        let teams = this.teams.map(x => x.id)
        let member: Member
        do {

            member = members[this.rnd(members.length - 1)]
            members.splice(members.findIndex(x => x.id === member.id), 1)

            const random = this.rnd(teams.length - 1)
            const team = this.teams.find(x => x.id === teams[random])!
            const teamIndex = teams.findIndex(x => x === team.id)

            if (team.members.length < 4) {
                this.teams.find(x => x.id === team.id)!.members.push(member)
                teams.splice(teamIndex, 1)
            }

            if (teams.length === 0)
                teams = this.teams.map(x => x.id)

        } while (members.length > 0)
    }

    calculateTeamDRS(): void {
        this.teams.forEach(team => {
            let totalScore = this.getMember(team.leaderId).score
            let score = 0
            team.members.forEach(member => {
                score = member.score
                totalScore += this.useMultiplier
                    ? score > 14 ? (score + (score * 1)) : (score + (score * 0.5))
                    : score
            })
            team.score = totalScore
        })
    }

    rnd(max: number): number {
        const min = 0
        return Math.floor(Math.random() * (max - min + 1)) + min
    }
}

export interface Team {
    id: number
    name: string
    logo: string
    leaderId: number
    score: number
    members: Member[]
}

export interface Member {
    id: number
    name: string
    groupLeader: boolean
    newbie: boolean
    mm: StatsSource
    faceit: StatsSource
    leetify: StatsSource
    csgoStats: StatsSource
    score: number
}

export interface StatsSource {
    uri: string
    level: number
}
