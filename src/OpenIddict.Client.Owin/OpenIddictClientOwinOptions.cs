﻿/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/openiddict/openiddict-core for more information concerning
 * the license and the contributors participating to this project.
 */

using Owin;

namespace OpenIddict.Client.Owin;

/// <summary>
/// Provides various settings needed to configure the OpenIddict OWIN client integration.
/// </summary>
public sealed class OpenIddictClientOwinOptions : AuthenticationOptions
{
    /// <summary>
    /// Creates a new instance of the <see cref="OpenIddictClientOwinOptions"/> class.
    /// </summary>
    public OpenIddictClientOwinOptions()
        : base(OpenIddictClientOwinDefaults.AuthenticationType)
        => AuthenticationMode = AuthenticationMode.Passive;

    /// <summary>
    /// Gets or sets a boolean indicating whether the static client registrations with a non-null
    /// provider name attached are automatically added to <see cref="ForwardedAuthenticationTypes"/>.
    /// When automatic forwarding is disabled, calls to <see cref="IAuthenticationManager"/> such as
    /// <see cref="IAuthenticationManager.Challenge(AuthenticationProperties, string[])"/>
    /// cannot directly use the provider name associated to a client registration as the authentication
    /// scheme and must set the provider name (or the issuer) as an authentication property instead.
    /// </summary>
    public bool DisableAutomaticAuthenticationTypeForwarding { get; set; }

    /// <summary>
    /// Gets the forwarded authentication types that are managed by the OpenIddict OWIN client host.
    /// </summary>
    public List<AuthenticationDescription> ForwardedAuthenticationTypes { get; } = [];

    /// <summary>
    /// Gets or sets a boolean indicating whether incoming requests arriving on insecure endpoints should be
    /// rejected and whether challenge and sign-out operations can be triggered from non-HTTPS endpoints.
    /// By default, this property is set to <see langword="false"/> to help mitigate man-in-the-middle attacks.
    /// </summary>
    public bool DisableTransportSecurityRequirement { get; set; }

    /// <summary>
    /// Gets or sets a boolean indicating whether the pass-through mode is enabled for the post-logout redirection endpoint.
    /// When the pass-through mode is used, OpenID Connect requests are initially handled by OpenIddict.
    /// Once validated, the rest of the request processing pipeline is invoked, so that OpenID Connect requests
    /// can be handled at a later stage (in a custom middleware or in a MVC controller, for instance).
    /// </summary>
    public bool EnablePostLogoutRedirectionEndpointPassthrough { get; set; }

    /// <summary>
    /// Gets or sets a boolean indicating whether the pass-through mode is enabled for the redirection endpoint.
    /// When the pass-through mode is used, OpenID Connect requests are initially handled by OpenIddict.
    /// Once validated, the rest of the request processing pipeline is invoked, so that OpenID Connect requests
    /// can be handled at a later stage (in a custom middleware or in a MVC controller, for instance).
    /// </summary>
    public bool EnableRedirectionEndpointPassthrough { get; set; }

    /// <summary>
    /// Gets or sets a boolean indicating whether OpenIddict should allow the rest of the request processing pipeline
    /// to be invoked when returning an error from the interactive authorization and end session endpoints.
    /// When this option is enabled, special logic must be added to these actions to handle errors, that can be
    /// retrieved using <see cref="OpenIddictClientOwinHelpers.GetOpenIddictClientResponse(IOwinContext)"/>.
    /// </summary>
    /// <remarks>
    /// Important: the error pass-through mode cannot be used when the status code pages integration is enabled.
    /// </remarks>
    public bool EnableErrorPassthrough { get; set; }

    /// <summary>
    /// Gets or sets the cookie manager used to read and write the cookies produced by the OWIN host.
    /// </summary>
    public ICookieManager CookieManager { get; set; } = new CookieManager();

    /// <summary>
    /// Gets or sets the name of the correlation cookie used to bind authorization
    /// responses with their original request and help mitigate authorization
    /// code injection, forged requests and session fixation attacks.
    /// </summary>
    public string CookieName { get; set; } = "OpenIddict.Client.State";

    /// <summary>
    /// Gets or sets the cookie options used to create the cookies that are used to
    /// bind authorization responses with their original request and help mitigate
    /// authorization code injection, forged requests and session fixation attacks.
    /// </summary>
    public CookieOptions CookieOptions { get; set; } = new()
    {
        HttpOnly = true,
        SameSite = SameSiteMode.None,
        Secure = true // Note: same-site=none requires using HTTPS.
    };
}
