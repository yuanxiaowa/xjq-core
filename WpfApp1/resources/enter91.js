var $user = $('#login_user');
if ($user.length > 0){
$('#login_user').val('{{Name}}');
$('#login_pass').val('{{Password}}')
checkLogin();
}