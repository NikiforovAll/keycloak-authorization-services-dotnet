import { defineConfig } from 'vitepress'
import { withMermaid } from "vitepress-plugin-mermaid";

// https://vitepress.dev/reference/site-config
export default withMermaid({
    title: "Keycloak.AuthServices",
    description: "",
    base: '/keycloak-authorization-services-dotnet/',
    themeConfig: {
        logo: '/logo.svg',
        // https://vitepress.dev/reference/default-theme-config
        nav: [
            { text: 'Home', link: '/' },
            { text: 'Getting Started', link: '/introduction' },
            { text: 'Migration', link: '/migration' },
            { text: 'Examples', link: 'examples/auth-getting-started' }
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
                    collapsed: false,
                    items: [
                        { text: 'Authentication', link: '/configuration/configuration-authentication' },
                        { text: 'Authorization', link: '/configuration/configuration-authorization' },
                        { text: 'Keycloak', link: '/configuration/configuration-keycloak' },
                    ]
                },
                {
                    text: 'Authorization',
                    collapsed: false,
                    items: [
                        { text: 'Authorization Server', link: '/authorization/authorization-server' },
                        { text: 'Protected Resources ✨', link: '/authorization/resources' },
                    ]
                },
                {
                    text: 'Examples',
                    collapsed: false,
                    items: [
                        { text: 'Auth Web API Getting Started', link: '/examples/auth-getting-started' },
                        { text: 'Authorization', link: '/examples/authorization-getting-started' },
                        { text: 'Auth Clean Architecture', link: '/examples/auth-clean-arch' },
                        { text: 'Auth Web API + Blazor', link: '/examples/web-api-blazor' }
                    ]
                }
            ]
        },
        socialLinks: [
            { icon: 'github', link: 'https://github.com/NikiforovAll/keycloak-authorization-services-dotnet' }
        ],
        editLink: {
            pattern: 'https://github.com/NikiforovAll/keycloak-authorization-services-dotnet/edit/main/docs/:path'
        }
    }
});
