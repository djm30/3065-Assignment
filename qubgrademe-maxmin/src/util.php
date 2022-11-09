<?php
function removeIncorrectModules($modules)
{
    return array_map(function ($module) {
        if (is_null($module)) return "";
        if (strlen(trim($module)) == 0) return "";
        else return $module;
    }, $modules);
}

function marksToInteger($marks)
{
    return array_map(function ($mark) {
        return intval($mark);
    }, $marks);
}
