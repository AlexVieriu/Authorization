namespace TimCorry_WASM_Authentification.Authentication;
public class JWTParser
{
    public static IEnumerable<Claim> ParseClaimsFromJWT(string jwt)
    {
        var claims = new List<Claim>();
        var payload = jwt.Split('.')[1];

        var jsonBytes =ParseBase64WithoutPadding(payload);

        var keyValueParis = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        ExtractRolesFromJWT(claims, keyValueParis);

        claims.AddRange(keyValueParis.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));

        return claims;
    }


    private static void ExtractRolesFromJWT(List<Claim> claims, Dictionary<string, object> keyValuesPairs)
    {
        keyValuesPairs.TryGetValue(ClaimTypes.Role, out object roles);

        if(roles is not null)
        {
            var parseRoles = roles.ToString().Trim().TrimStart('[').TrimEnd(']').Split(',');

            if(parseRoles.Length >1)
            {
                foreach (var parseRole in parseRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, parseRole.Trim('"')));
                }
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, parseRoles[0]));
            }

            keyValuesPairs.Remove(ClaimTypes.Role);
        }
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4) // 5/3 = 2; 3/2 = 1;
        {
            case 2:
                base64 += "==";
                break;
            case 3:
                base64 += "=";
                break;
        }
        return Convert.FromBase64String(base64);
    }
}
