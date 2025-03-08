using System.Web.UI;

public static class NotificationHelper
{
    /// <summary>
    /// Displays a centered notification box with a message, type, and optional title.
    /// </summary>
    /// <param name="page">The current Page instance.</param>
    /// <param name="message">The message to display in the notification box.</param>
    /// <param name="type">The type of notification: "success", "error", "warning", or "info".</param>
    /// <param name="title">The title of the notification (optional, defaults to "Notification").</param>
    public static void ShowNotification(Page page, string message, string type, string title = "Notification")
    {
        // Map the type to a Bootstrap color class
        string notificationTypeClass = GetNotificationTypeClass(type);

        // Generate the notification box HTML
        string notificationHtml = @"
        <div id='dynamicNotification' class='notification-box bg-light rounded shadow-sm p-4' style='
        position: fixed;
        top: 50%;
        left: 50%;
        transform: translate(-50%, -50%);
        z-index: 1050;
        padding: 20px;
        border-radius: 12px;
        box-shadow: 0 10px 20px rgba(0, 0, 0, 0.2);
        text-align: center;
        width: 350px;
        max-width: 90%;
        display: none;
        background-color: #ffffff;
        color: #333;
        '>
        <div style='
            font-size: 18px;
            font-weight: bold;
            margin-bottom: 10px;
            color: " + GetNotificationTextColor(notificationTypeClass) + @";
        '>
            " + EscapeForJavaScript(title) + @"
        </div>
        <div style='
            font-size: 16px;
            line-height: 1.5;
            margin-bottom: 20px;
        '>
            " + EscapeForJavaScript(message) + @"
        </div>
        <button id='notificationCloseButton' class='btn btn-primary btn-sm' style='
            padding: 8px 16px;
            font-size: 14px;
            border: none;
            border-radius: 4px;
            background-color: " + GetNotificationButtonColor(notificationTypeClass) + @";
            color: #fff;
            cursor: pointer;
        '>
            Dismiss
        </button>
        </div>";

        // Generate JavaScript for displaying the notification box
        string script = @"
            (function() {
                // Append the notification box to the body
                var notificationContainer = document.createElement('div');
                notificationContainer.innerHTML = `" + notificationHtml + @"`;
                document.body.appendChild(notificationContainer);

                // Show the notification box
                var notificationEl = document.getElementById('dynamicNotification');
                notificationEl.style.display = 'block';

                // Close the notification box when the button is clicked
                document.getElementById('notificationCloseButton').addEventListener('click', function () {
                    notificationEl.remove();
                });
            })();
        ";

        // Register the script on the page
        page.ClientScript.RegisterStartupScript(page.GetType(), "NotificationScript", script, true);
    }

    /// <summary>
    /// Maps the type to the appropriate Bootstrap color class.
    /// </summary>
    private static string GetNotificationTypeClass(string type)
    {
        switch (type.ToLower())
        {
            case "success": return "success";
            case "error": return "danger";
            case "warning": return "warning";
            case "info": return "info";
            default: return "secondary";
        }
    }

    private static string GetNotificationTextColor(string type)
    {
        switch (type.ToLower())
        {
            case "success": return "#155724"; // Dark green
            case "error": return "#721c24"; // Dark red
            case "warning": return "#856404"; // Dark yellow
            case "info": return "#0c5460"; // Dark blue
            default: return "#333333"; // Default dark gray
        }
    }

    private static string GetNotificationButtonColor(string type)
    {
        switch (type.ToLower())
        {
            case "success": return "#28a745"; // Green
            case "error": return "#dc3545"; // Red
            case "warning": return "#ffc107"; // Yellow
            case "info": return "#17a2b8"; // Blue
            default: return "#6c757d"; // Default gray
        }
    }


    /// <summary>
    /// Escapes strings for safe use in JavaScript.
    /// </summary>
    private static string EscapeForJavaScript(string input)
    {
        if (string.IsNullOrEmpty(input)) return string.Empty;
        return input.Replace("'", "\\'").Replace("\"", "\\\"");
    }
}
