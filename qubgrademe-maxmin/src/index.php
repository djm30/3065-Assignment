<?php
header("Access-Control-Allow-Origin: *");
header("Content-type: application/json");
require("functions.inc.php");
require("util.inc.php");
include("validation.inc.php");

$request = explode("?", $_SERVER["REQUEST_URI"])[0];

function handleMinMax()
{
	$output = array(
		"error" => false,
		"errorMessage" => "",
		"modules" => "",
		"marks" => 0,
		"max_module" => "",
		"min_module" => ""
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

	$max_min_modules = array("", "");
	// If validation passesd, message length will be 0
	if (strlen($validation_message) == 0) {
		header("HTTP/1.1 200 OK");
		$max_min_modules = getMaxMin($modules, $marks);
	} else {
		header("HTTP/1.1 400 Bad Request");
		$output["error"] = true;
		$output["errorMessage"] = $validation_message;
	}

	$modules = removeIncorrectModules($modules);
	$marks = marksToInteger($marks);


	$output['modules'] = $modules;
	$output['marks'] = $marks;
	$output['max_module'] = $max_min_modules[0];
	$output['min_module'] = $max_min_modules[1];

	echo json_encode($output);
	@exit();
}

function handleHealth()
{
}

switch ($request) {
	case "/health":
		handleHealth();
		break;

	case "/":
		handleMinMax();
		break;
}
