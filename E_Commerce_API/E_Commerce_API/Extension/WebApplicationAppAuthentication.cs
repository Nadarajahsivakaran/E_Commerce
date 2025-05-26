using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Product.Extension
{
    public static class WebApplicationAppAuthentication
    {
        public static WebApplicationBuilder AddAppAuthentication(this WebApplicationBuilder builder)
        {
            IConfigurationSection tokenCredential = builder.Configuration.GetSection("TokenCredential:JwtOptions");
            string? secrete = tokenCredential.GetValue<string>("Secrete");
            string? issuer = tokenCredential.GetValue<string>("Issuer");
            string? audience = tokenCredential.GetValue<string>("Audience");

            byte[] key = Encoding.ASCII.GetBytes(secrete);

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience,
                    ClockSkew = TimeSpan.Zero
                };
            });
            builder.Services.AddAuthorization();
            return builder;
        }
    }
}
