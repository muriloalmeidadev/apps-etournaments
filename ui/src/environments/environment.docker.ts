export const environment = {
    production: true,
    name: 'docker',
    title: 'eTournaments',
    authServer: {
        url: 'http://host.docker.internal:9999/auth',
        realm: 'development',
        clientId: 'development',
        scopes: 'openid profile email roles',
    },
}
