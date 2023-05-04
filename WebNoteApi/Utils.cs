using Storage.DB;

public static class Utils {
    public static string NewId() {
        return Guid.NewGuid().ToString();
    }
}
