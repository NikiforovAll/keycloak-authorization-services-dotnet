namespace Keycloak.AuthServices.Authorization.Tests.Organizations;

using System.Security.Claims;
using FluentAssertions;
using Keycloak.AuthServices.Common.Claims;

public class OrganizationClaimsExtensionsTests
{
    private const string JsonValueType = "JSON";
    private const string Issuer = "https://keycloak.example.com/realms/test";

    [Fact]
    public void GetOrganizations_SingleOrg_ReturnsOne()
    {
        var principal = CreatePrincipal(
            /*lang=json,strict*/
            """
            {
                "acme-corp": {}
            }
            """
        );

        var orgs = principal.GetOrganizations();

        orgs.Should().HaveCount(1);
        orgs[0].Alias.Should().Be("acme-corp");
        orgs[0].Id.Should().BeNull();
        orgs[0].Attributes.Should().BeNull();
    }

    [Fact]
    public void GetOrganizations_MultipleOrgs_ReturnsAll()
    {
        var principal = CreatePrincipal(
            /*lang=json,strict*/
            """
            {
                "acme-corp": {},
                "partner-inc": {}
            }
            """
        );

        var orgs = principal.GetOrganizations();

        orgs.Should().HaveCount(2);
        orgs.Select(o => o.Alias).Should().Contain(["acme-corp", "partner-inc"]);
    }

    [Fact]
    public void GetOrganizations_WithId_ParsesId()
    {
        var principal = CreatePrincipal(
            /*lang=json,strict*/
            """
            {
                "acme-corp": {
                    "id": "a56bea03-5904-470a-b21c-92b7f1069d44"
                }
            }
            """
        );

        var orgs = principal.GetOrganizations();

        orgs.Should().HaveCount(1);
        orgs[0].Alias.Should().Be("acme-corp");
        orgs[0].Id.Should().Be("a56bea03-5904-470a-b21c-92b7f1069d44");
    }

    [Fact]
    public void GetOrganizations_WithAttributes_ParsesAttributes()
    {
        var principal = CreatePrincipal(
            /*lang=json,strict*/
            """
            {
                "testcorp": {
                    "id": "42c3e46f-2477-44d7-a85b-d3b43f6b31fa",
                    "tier": ["premium"],
                    "regions": ["us-east", "eu-west"]
                }
            }
            """
        );

        var orgs = principal.GetOrganizations();

        orgs.Should().HaveCount(1);
        orgs[0].Alias.Should().Be("testcorp");
        orgs[0].Id.Should().Be("42c3e46f-2477-44d7-a85b-d3b43f6b31fa");
        orgs[0].Attributes.Should().NotBeNull();
        orgs[0].Attributes!["tier"].Should().Equal("premium");
        orgs[0].Attributes!["regions"].Should().Equal("us-east", "eu-west");
    }

    [Fact]
    public void GetOrganizations_MissingClaim_ReturnsEmpty()
    {
        var principal = new ClaimsPrincipal(new ClaimsIdentity());

        var orgs = principal.GetOrganizations();

        orgs.Should().BeEmpty();
    }

    [Fact]
    public void GetOrganizations_EmptyObject_ReturnsEmpty()
    {
        var principal = CreatePrincipal( /*lang=json,strict*/
            "{}"
        );

        var orgs = principal.GetOrganizations();

        orgs.Should().BeEmpty();
    }

    [Fact]
    public void IsMemberOf_ExistingAlias_ReturnsTrue()
    {
        var principal = CreatePrincipal(
            /*lang=json,strict*/
            """
            {
                "acme-corp": { "id": "uuid-1" },
                "partner-inc": { "id": "uuid-2" }
            }
            """
        );

        principal.IsMemberOf("acme-corp").Should().BeTrue();
        principal.IsMemberOf("partner-inc").Should().BeTrue();
    }

    [Fact]
    public void IsMemberOf_NonExistingAlias_ReturnsFalse()
    {
        var principal = CreatePrincipal(
            /*lang=json,strict*/
            """
            {
                "acme-corp": {}
            }
            """
        );

        principal.IsMemberOf("other-org").Should().BeFalse();
    }

    [Fact]
    public void IsMemberOf_CaseInsensitive()
    {
        var principal = CreatePrincipal(
            /*lang=json,strict*/
            """
            {
                "Acme-Corp": {}
            }
            """
        );

        principal.IsMemberOf("acme-corp").Should().BeTrue();
    }

    [Fact]
    public void IsMemberOfById_ExistingId_ReturnsTrue()
    {
        var principal = CreatePrincipal(
            /*lang=json,strict*/
            """
            {
                "acme-corp": { "id": "a56bea03-5904-470a-b21c-92b7f1069d44" }
            }
            """
        );

        principal.IsMemberOfById("a56bea03-5904-470a-b21c-92b7f1069d44").Should().BeTrue();
    }

    [Fact]
    public void IsMemberOfById_NonExistingId_ReturnsFalse()
    {
        var principal = CreatePrincipal(
            /*lang=json,strict*/
            """
            {
                "acme-corp": { "id": "a56bea03-5904-470a-b21c-92b7f1069d44" }
            }
            """
        );

        principal.IsMemberOfById("00000000-0000-0000-0000-000000000000").Should().BeFalse();
    }

    [Fact]
    public void IsMemberOfById_NoIdInClaim_ReturnsFalse()
    {
        var principal = CreatePrincipal(
            /*lang=json,strict*/
            """
            {
                "acme-corp": {}
            }
            """
        );

        principal.IsMemberOfById("any-id").Should().BeFalse();
    }

    [Fact]
    public void GetOrganizations_MultipleOrgsWithMixedData_ParsesCorrectly()
    {
        var principal = CreatePrincipal(
            /*lang=json,strict*/
            """
            {
                "acme-corp": {
                    "id": "uuid-1",
                    "tier": ["enterprise"]
                },
                "partner-inc": {},
                "startup-co": {
                    "id": "uuid-3"
                }
            }
            """
        );

        var orgs = principal.GetOrganizations();

        orgs.Should().HaveCount(3);

        var acme = orgs.Single(o => o.Alias == "acme-corp");
        acme.Id.Should().Be("uuid-1");
        acme.Attributes.Should().ContainKey("tier");

        var partner = orgs.Single(o => o.Alias == "partner-inc");
        partner.Id.Should().BeNull();
        partner.Attributes.Should().BeNull();

        var startup = orgs.Single(o => o.Alias == "startup-co");
        startup.Id.Should().Be("uuid-3");
        startup.Attributes.Should().BeNull();
    }

    [Fact]
    public void GetOrganizations_VanillaFormat_MultipleStringClaims_ReturnsAll()
    {
        var principal = CreatePrincipalWithStringClaims("acme-corp", "startup-co");

        var orgs = principal.GetOrganizations();

        orgs.Should().HaveCount(2);
        orgs.Select(o => o.Alias).Should().Contain(["acme-corp", "startup-co"]);
        orgs.Should()
            .AllSatisfy(o =>
            {
                o.Id.Should().BeNull();
                o.Attributes.Should().BeNull();
            });
    }

    [Fact]
    public void GetOrganizations_VanillaFormat_SingleStringClaim_ReturnsOne()
    {
        var principal = CreatePrincipalWithStringClaims("acme-corp");

        var orgs = principal.GetOrganizations();

        orgs.Should().HaveCount(1);
        orgs[0].Alias.Should().Be("acme-corp");
    }

    [Fact]
    public void IsMemberOf_VanillaFormat_ExistingAlias_ReturnsTrue()
    {
        var principal = CreatePrincipalWithStringClaims("acme-corp", "partner-inc");

        principal.IsMemberOf("acme-corp").Should().BeTrue();
        principal.IsMemberOf("partner-inc").Should().BeTrue();
    }

    [Fact]
    public void IsMemberOf_VanillaFormat_NonExistingAlias_ReturnsFalse()
    {
        var principal = CreatePrincipalWithStringClaims("acme-corp");

        principal.IsMemberOf("other-org").Should().BeFalse();
    }

    [Fact]
    public void IsMemberOf_VanillaFormat_CaseInsensitive()
    {
        var principal = CreatePrincipalWithStringClaims("Acme-Corp");

        principal.IsMemberOf("acme-corp").Should().BeTrue();
    }

    [Fact]
    public void GetOrganizations_CustomClaimType_ParsesCorrectly()
    {
        var principal = CreatePrincipal(
            /*lang=json,strict*/
            """
            {
                "acme-corp": { "id": "uuid-1" }
            }
            """,
            claimType: "tenant"
        );

        var orgs = principal.GetOrganizations("tenant");

        orgs.Should().HaveCount(1);
        orgs[0].Alias.Should().Be("acme-corp");
        orgs[0].Id.Should().Be("uuid-1");
    }

    [Fact]
    public void GetOrganizations_CustomClaimType_DefaultDoesNotMatch()
    {
        var principal = CreatePrincipal(
            /*lang=json,strict*/
            """
            {
                "acme-corp": {}
            }
            """,
            claimType: "tenant"
        );

        var orgs = principal.GetOrganizations();

        orgs.Should().BeEmpty();
    }

    [Fact]
    public void IsMemberOf_CustomClaimType_ReturnsTrue()
    {
        var principal = CreatePrincipalWithStringClaims(["acme-corp"], claimType: "org");

        principal.IsMemberOf("acme-corp", "org").Should().BeTrue();
    }

    [Fact]
    public void IsMemberOf_CustomClaimType_DefaultDoesNotMatch()
    {
        var principal = CreatePrincipalWithStringClaims(["acme-corp"], claimType: "org");

        principal.IsMemberOf("acme-corp").Should().BeFalse();
    }

    private static ClaimsPrincipal CreatePrincipal(
        string organizationClaimValue,
        string claimType = "organization"
    ) =>
        new(
            new ClaimsIdentity(
                [new Claim(claimType, organizationClaimValue, JsonValueType, Issuer, Issuer)],
                "Bearer"
            )
        );

    private static ClaimsPrincipal CreatePrincipalWithStringClaims(params string[] orgAliases) =>
        CreatePrincipalWithStringClaims(orgAliases, claimType: "organization");

    private static ClaimsPrincipal CreatePrincipalWithStringClaims(
        string[] orgAliases,
        string claimType = "organization"
    ) => new(new ClaimsIdentity(orgAliases.Select(alias => new Claim(claimType, alias)), "Bearer"));
}
