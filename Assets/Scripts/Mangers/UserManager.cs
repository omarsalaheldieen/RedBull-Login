public class UserManager
{
    public static string Username { get; private set; }
    public static string AccessToken { get; private set; }
    public static int Uuid { get; private set; }
    public static string AdminAccessToken { get; private set; }
    public static int Id { get; private set; }
    public static int Score { get; private set; }
    public static string Rank { get; private set; }

    public static void SetUserDetails(string username, string accessToken, int uuid, int id)
    {
        Username = username;
        AccessToken = accessToken;
        Uuid = uuid;
        Id = id;
    }

    public static void SetAdminAccessToken(string accessToken)
    {
        AdminAccessToken = accessToken;
    }

    public static void SetUserScore(int score , string rank)
    {
        Score = score;
        Rank= rank; 
    }
}