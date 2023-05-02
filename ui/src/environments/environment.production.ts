export const environment = {
    production: true,
    name: 'live',
    title: 'WSCD 2023',
    authServer: {
        url: 'http://live-keycloak-instance/auth',
        realm: 'dev-service',
        clientId: 'development',
        scopes: 'openid profile email roles',
    },
}
