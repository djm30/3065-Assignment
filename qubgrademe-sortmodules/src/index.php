<?php
header("Access-Control-Allow-Origin: *");
header("Content-type: application/json");
require("functions.inc.php");
require("validation.inc.php");
require("util.inc.php");


$output = array(
	"error" => false,
	"errorMessage" => "",
	"modules" => "",
	"marks" => 0,
	"sorted_modules" => ""
);


$module_1 = @$_REQUEST['module_1'];
$module_2 = @$_REQUEST['module_2'];
$module_3 = @$_REQUEST['module_3'];
$module_4 = @$_REQUEST['module_4'];
$module_5 = @$_REQUEST['module_5'];
$mark_1 = @$_REQUEST['mark_1'];
$mark_2 = @$_REQUEST['mark_2'];
$mark_3 = @$_REQUEST['mark_3'];
$mark_4 = @$_REQUEST['mark_4'];
$mark_5 = @$_REQUEST['mark_5'];

$modules = array($module_1, $module_2, $module_3, $module_4, $module_5);
$marks = array($mark_1, $mark_2, $mark_3, $mark_4, $mark_5);

$validation_message = validate($modules, $marks);

$modules = removeIncorrectModules($modules);
$marks = marksToInteger($marks);

$sorted_modules = "";

// If validation passesd, message length will be 0
if (strlen($validation_message) == 0) {
	header("HTTP/1.1 200 OK");
	$sorted_modules = getSortedModules($modules, $marks);
} else {
	header("HTTP/1.1 400 Bad Request");
	$output["error"] = true;
	$output["errorMessage"] = $validation_message;
}

$output['modules'] = $modules;
$output['marks'] = $marks;
$output['sorted_modules'] = $sorted_modules;


echo json_encode($output);
exit();
