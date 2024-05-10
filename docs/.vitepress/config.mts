import { defineConfig } from 'vitepress'
import { withMermaid } from "vitepress-plugin-mermaid";

// https://vitepress.dev/reference/site-config
export default withMermaid({
    title: "Keycloak.AuthServices",
    description: "",
    base: '/keycloak-authorization-services-dotnet/',
    head: [
        ["link", { rel: "icon", type: "image/png", sizes: "16x16", href: "/favicon-16x16.png" }],
        ["link", { rel: "icon", type: "image/png", sizes: "32x32", href: "/favicon-32x32.png" }],
        ["link", { rel: "manifest", href: "/site.webmanifest" }]
    ],
    themeConfig: {
        logo: '/logo.svg',
        // https://vitepress.dev/reference/default-theme-config
        nav: [
            { text: 'Home', link: '/' },
            { text: 'Getting Started', link: '/introduction' },
            { text: 'Migration', link: '/migration' },
            { text: 'Examples', link: 'examples/auth-getting-started' },
            { text: 'Blog', link: 'blogs' },
            { text: 'Join Chat', link: 'https://discord.gg/jdYFw2xq' },
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
                    collapsed: true,
                    items: [
                        {
                            text: 'Authorization Server', link: '/authorization/authorization-server'
                        },
                        {
                            text: 'Protected Resources ✨', link: '/authorization/resources',
                            collapsed: true,
                            items: [
                                { text: 'ASP.NET Core Integration', link: '/authorization/resources-api' },
                                {
                                    text: 'Protected Resource Builder', link: '/authorization/protected-resource-builder', items: [
                                        { text: 'MVC-style projects', link: '/authorization/protected-resource-builder-mvc' },
                                    ]
                                },
                                { text: 'Policy Provider', link: '/authorization/policy-provider' },
                            ]
                        },
                        {
                            text: 'Use Authorization Client', link: '/authorization/resources-client',
                            collapsed: true,
                            items: [
                                { text: 'HTTP Client', link: '/authorization/resources-client' },
                                { text: 'Client API Reference', link: '/authorization/resources-client-reference' },
                            ]
                        },
                    ]
                },
                {
                    text: 'Admin REST API ⚙️',
                    collapsed: true,
                    items: [
                        { text: 'Introduction', link: '/admin-rest-api/admin-rest-api' },
                        { text: 'Access Token Management', link: '/admin-rest-api/access-token' },
                        {
                            text: 'Admin API Reference', link: '/admin-rest-api/admin-api-reference',
                            items: [
                                { text: 'Realm Client', link: '/admin-rest-api/realm-client' },
                                { text: 'User Client', link: '/admin-rest-api/user-client' },
                                { text: 'Group Client', link: '/admin-rest-api/group-client' },
                            ]
                        },
                        { text: 'OpenAPI Support', link: '/admin-rest-api/admin-api-openapi' },
                        { text: 'Generated Client - Kiota', link: '/admin-rest-api/admin-api-kiota' },
                    ]
                },
                {
                    text: 'Protection API ⚙️',
                    collapsed: true,
                    items: [
                        { text: 'Introduction', link: '/protection-api/protection-api' },
                        {
                            text: 'Protection API Reference', link: '/protection-api/protection-api-reference',
                            items: [
                                { text: 'Protected Resource Client', link: '/protection-api/protected-resource-client' },
                                { text: 'Policy Client', link: '/protection-api/policy-client' },
                            ]
                        },
                    ]
                },
                {
                    text: 'Q&A',
                    items: [
                        { text: 'Recipes', link: '/qa/recipes' },
                        { text: 'Troubleshooting', link: '/qa/troubleshooting' },
                    ]
                },
                {
                    text: 'Examples',
                    collapsed: false,
                    items: [
                        { text: 'Web API Getting Started', link: '/examples/auth-getting-started' },
                        { text: 'Authorization', link: '/examples/authorization-getting-started' },
                        { text: 'Resource Authorization ✨', link: '/examples/resource-authorization' },
                        { text: 'Clean Architecture', link: '/examples/auth-clean-arch' },
                        { text: 'Web API + Blazor', link: '/examples/web-api-blazor' }
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
