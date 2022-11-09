<?php


header("Access-Control-Allow-Origin: *");
header("Content-type: application/json");
header("HTTP/1.1 200 OK");


$str   = @file_get_contents('/proc/uptime');
$num   = floatval($str);

$output = array(
    "date" => date("Y-m-d H:i:s.u"),
    "message" => "Ok",
    "uptime" => $num,
);
echo json_encode($output);
exit();
