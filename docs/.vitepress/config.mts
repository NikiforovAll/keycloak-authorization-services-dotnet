import { defineConfig } from 'vitepress'

// https://vitepress.dev/reference/site-config
export default defineConfig({
    title: "Keycloak.AuthServices",
    description: "",
    base: '/keycloak-authorization-services-dotnet/',
    themeConfig: {
        // https://vitepress.dev/reference/default-theme-config
        nav: [
            { text: 'Home', link: '/' },
            { text: 'Getting Started', link: '/getting-started' },
            { text: 'Examples', link: 'examples/authentication-getting-started' }
        ],

        sidebar: {
            '/': [
                {
                    text: 'Introduction',
                    collapsed: false,
                    items: [
                        { text: 'What is Keycloak.AuthServices?', link: '/introduction' },
                        { text: 'Getting Started', link: '/getting-started' }
                    ]
                },
                {
                    text: 'Configuration',
                    collapsed: true,
                    items: [

                    ]
                }
                ,
                {
                    text: 'Examples',
                    collapsed: false,
                    items: [
                        { text: 'Authentication Web API', link: '/examples/authentication-getting-started' },
                        { text: 'Authentication Web App', link: '/examples/authentication-getting-started' },
                        { text: 'Authorization Roles', link: '/examples/authorization-getting-started' },
                        { text: 'Authorization Server', link: '/examples/authorization-getting-started' }
                    ]
                }
            ]
        },
        socialLinks: [
            { icon: 'github', link: 'https://github.com/NikiforovAll/keycloak-authorization-services-dotnet' }
        ]
    }
})
