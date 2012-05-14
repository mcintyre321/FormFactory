@model $rootnamespace$.Models.ChangePasswordModel
@{
    ViewBag.Title = "Change Password";
}
<div class="page-header">
    <h2>
        Change Password</h2>
</div>
<p>
    Use the form below to change your password.
</p>
<p>
    New passwords are required to be a minimum of @Membership.MinRequiredPasswordLength
    characters in length.
</p>
<script src="@Url.Content("~/Scripts/jquery.validate.min.js")" type="text/javascript"></script>
<script src="@Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js")" type="text/javascript"></script>
@using (Html.BeginForm())
{
    @Html.ValidationSummary(true, "Password change was unsuccessful. Please correct the errors and try again.")
    <div>
        <fieldset>
            <legend>Account Information</legend>
            <div class="control-group">
                @Html.LabelFor(m => m.OldPassword)
                <div class="controls">
                    @Html.PasswordFor(m => m.OldPassword)
                    @Html.ValidationMessageFor(m => m.OldPassword)
                </div>
            </div>
            <div class="control-group">
                @Html.LabelFor(m => m.NewPassword)
                <div class="controls">
                    @Html.PasswordFor(m => m.NewPassword)
                    @Html.ValidationMessageFor(m => m.NewPassword)
                </div>
            </div>
            <div class="control-group">
                @Html.LabelFor(m => m.ConfirmPassword)
                <div class="controls">
                    @Html.PasswordFor(m => m.ConfirmPassword)
                    @Html.ValidationMessageFor(m => m.ConfirmPassword)
                </div>
            </div>
            <div class="form-actions">
                <input type="submit" class="btn btn-primary" value="Change Password" />
            </div>
        </fieldset>
    </div>
}
