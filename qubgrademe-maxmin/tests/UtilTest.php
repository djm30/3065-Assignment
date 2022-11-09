<?php
require __DIR__ . "/../src/util.php";

use \PHPUnit\Framework\TestCase;

class TestRemoveIncorrectModules extends TestCase
{
    /** @test */
    public function all_valid_strings_returns_same_array_values()
    {
        $modules = array("CSC 3021", "CSC 3059", "CSC 3063", "CSC 3065", "CSC 3068");
        $result = removeIncorrectModules($modules);
        $this->assertEquals($modules, $result);
    }

    /** @test */
    public function null_is_replaced_with_an_empty_string()
    {
        $modules = array(null, "CSC 3059", "CSC 3063", "CSC 3065", "CSC 3068");
        $expected = array("", "CSC 3059", "CSC 3063", "CSC 3065", "CSC 3068");
        $result = removeIncorrectModules($modules);
        $this->assertEquals($expected, $result);
    }

    /** @test */
    public function whitespace_is_replaced_with_an_empty_string()
    {
        $modules = array("  ", "CSC 3059", "CSC 3063", "CSC 3065", "CSC 3068");
        $expected = array("", "CSC 3059", "CSC 3063", "CSC 3065", "CSC 3068");
        $result = removeIncorrectModules($modules);
        $this->assertEquals($expected, $result);
    }
}

class TestMarksToInger extends TestCase
{
    /** @test */
    public function strings_are_converted_to_ints()
    {
        $marks = array("63", "64", "65", "66", "67");
        $expected = array(63, 64, 65, 66, 67);
        $result = marksToInteger($marks);
        $this->assertEquals($expected, $result);
    }

    /** @test */
    public function array_of_ints_is_unchanged()
    {
        $marks = array(63, 64, 65, 66, 67);
        $result = marksToInteger($marks);
        $this->assertEquals($marks, $result);
    }

    /** @test */
    public function null_values_are_changed_to_zero()
    {
        $marks = array(null, "64", "65", "66", "67");
        $expected = array(0, 64, 65, 66, 67);
        $result = marksToInteger($marks);
        $this->assertEquals($expected, $result);
    }

    /** @test */
    public function all_null_values_replaced_with_zero()
    {
        $marks = array(null, null, null, null, null);
        $expected = array(0, 0, 0, 0, 0);
        $result = marksToInteger($marks);
        $this->assertEquals($expected, $result);
    }
}
