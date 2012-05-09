@model $rootnamespace$.Models.LogOnModel
@{
    ViewBag.Title = "Log On";
}
<div class="page-header">
    <h2>
        Log On</h2>
</div>
<p>
    Please enter your user name and password. @Html.ActionLink("Register", "Register")
    if you don't have an account.
</p>
<script src="@Url.Content("~/Scripts/jquery.validate.min.js")" type="text/javascript"></script>
<script src="@Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js")" type="text/javascript"></script>
@Html.ValidationSummary(true, "Login was unsuccessful. Please correct the errors and try again.")
@using (Html.BeginForm())
{
    <div>
        <fieldset>
            <legend>Account Information</legend>
            <div class="control-group">
                @Html.LabelFor(m => m.UserName)
                <div class="controls">
                    @Html.TextBoxFor(m => m.UserName, new { @class = "span4" })
                    @Html.ValidationMessageFor(m => m.UserName)
                </div>
            </div>
            <div class="control-group">
                @Html.LabelFor(m => m.Password)
                <div class="controls">
                    @Html.PasswordFor(m => m.Password, new { @class = "span4" })
                    @Html.ValidationMessageFor(m => m.Password)
                </div>
            </div>
            <div class="control-group">
                <div class="controls">
                    <label class="checkbox">
                        @Html.CheckBoxFor(m => m.RememberMe)
                        <span>Remember Me</span>
                    </label>
                </div>
            </div>
            <div class="form-actions">
                <button type="submit" class="btn btn-primary">
                    Log On</button>
            </div>
        </fieldset>
    </div>
}
