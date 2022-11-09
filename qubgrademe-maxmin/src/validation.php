<?php

function validate($modules, $marks)
{
    $message = "";
    if (allNullOrEmpty($modules))
        $message = "Please provide some module codes and their respective marks";

    if (strlen($message) == 0) {
        for ($i = 0; $i < count($modules); $i++) {
            // If there is a valid module at this index
            if (!isModuleNullOrEmpty($modules[$i])) {
                // Check if the mark provided is also valid
                if (!validIntegerInRange($marks[$i])) {
                    $message =
                        "Please provide a valid integer for every entered module";
                    break;
                }
            }
            // If there is not a valid module at this index
            else {
                // Check if there has been a valid mark provided
                if (validIntegerInRange($marks[$i])) {
                    // If there is, send back an error
                    $message = "Please provide a module name for all marks entered";
                    break;
                }
            }
        }
    }

    return $message;
}

function allNullOrEmpty($array)
{
    $areAllNullEmpty = true;
    foreach ($array as $value) {
        if (!isModuleNullOrEmpty($value)) {
            $areAllNullEmpty = false;
        }
    }
    return $areAllNullEmpty;
}

function isModuleNullOrEmpty($value)
{
    return is_null($value) or strlen(trim($value)) == 0;
}

function validIntegerInRange($value)
{
    if (is_null($value)) return false;
    if (is_numeric($value)) {
        $int_value = intval($value);
        if ($int_value <= 100 and $int_value >= 0) {
            return true;
        }
    }
    return false;
}
