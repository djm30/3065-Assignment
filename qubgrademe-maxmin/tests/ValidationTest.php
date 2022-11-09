<?php

require_once __DIR__ . "/../src/validation.php";

use \PHPUnit\Framework\TestCase;

class TestIsValidInteger extends  TestCase
{
    /** @test */
    public function string_100_is_valid_integer()
    {
        $result = validIntegerInRange("100");
        $this->assertTrue($result);
    }

    /** @test */
    public function string_0_is_valid_integer()
    {
        $result = validIntegerInRange("0");
        $this->assertTrue($result);
    }

    /** @test */
    public function string_50_is_valid_integer()
    {
        $result = validIntegerInRange("50");
        $this->assertTrue($result);
    }

    /** @test */
    public function string_hello_is_not_valid_integer()
    {
        $result = validIntegerInRange("hello");
        $this->assertFalse($result);
    }

    /** @test */
    public function string_101_is_not_valid_integer()
    {
        $result = validIntegerInRange("101");
        $this->assertFalse($result);
    }

    /** @test */
    public function string_minus_1_is_not_valid_integer()
    {
        $result = validIntegerInRange("-1");
        $this->assertFalse($result);
    }

    /** @test */
    public function null_is_not_a_valid_integer()
    {
        $result = validIntegerInRange(null);
        $this->assertFalse($result);
    }

    /** @test */
    public function works_if_value_is_already_an_integer()
    {
        $result = validIntegerInRange(10);
        $this->assertTrue($result);
    }
}

class TestIsModuleNullOrEmpty extends  TestCase
{
    /** @test */
    public function valid_string_returns_false()
    {
        $result = isModuleNullOrEmpty("CSC 3068");
        $this->assertFalse($result);
    }

    /** @test */
    public function zero_length_string_returns_true()
    {
        $result = isModuleNullOrEmpty("");
        $this->assertTrue($result);
    }

    /** @test */
    public function whitespace_string_returns_true()
    {
        $result = isModuleNullOrEmpty(" ");
        $this->assertTrue($result);
    }

    /** @test */
    public function null_returns_true()
    {
        $result = isModuleNullOrEmpty(null);
        $this->assertTrue($result);
    }
}

class TestAreModulesNullOrEmpty extends  TestCase
{
    /** @test */
    public function all_valid_strings_returns_false()
    {
        $result = allNullOrEmpty(array("CSC 3021", "CSC 3059", "CSC 3063", "CSC 3065", "CSC 3068"));
        $this->assertFalse($result);
    }

    /** @test */
    public function one_null_value_returns_false()
    {
        $result = allNullOrEmpty(array("CSC 3021", null, "CSC 3063", "CSC 3065", "CSC 3068"));
        $this->assertFalse($result);
    }

    /** @test */
    public function one_zero_length_string_value_returns_false()
    {
        $result = allNullOrEmpty(array("CSC 3021", "", "CSC 3063", "CSC 3065", "CSC 3068"));
        $this->assertFalse($result);
    }

    /** @test */
    public function one_whitespace_string_value_returns_false()
    {
        $result = allNullOrEmpty(array("CSC 3021", "  ", "CSC 3063", "CSC 3065", "CSC 3068"));
        $this->assertFalse($result);
    }

    /** @test */
    public function all_null_returns_true()
    {
        $result = allNullOrEmpty(array(null, null, null, null, null));
        $this->assertTrue($result);
    }

    /** @test */
    public function all_zero_length_strings_returns_true()
    {
        $result = allNullOrEmpty(array("", "", "", "", ""));
        $this->assertTrue($result);
    }

    /** @test */
    public function all_whitespace_length_strings_returns_true()
    {
        $result = allNullOrEmpty(array(" ", " ", " ", " ", " "));
        $this->assertTrue($result);
    }

    /** @test */
    public function mix_of_above_returns_true()
    {
        $result = allNullOrEmpty(array("", null, "", "", " "));
        $this->assertTrue($result);
    }
}

class TestValidate extends TestCase
{
    /** @test */
    public function succeeds_with_valid_modules_and_marks()
    {
        $modules = array("CSC 3021", "CSC 3059", "CSC 3063", "CSC 3065", "CSC 3068");
        $marks = array("70", "63", "70", "50", "60");
        $result = validate($modules, $marks);
        $this->assertEquals("", $result);
    }

    /** @test */
    public function succeeds_with_a_missing_pair_of_modules_and_marks()
    {
        $modules = array("CSC 3021", "CSC 3059", null, "CSC 3065", "CSC 3068");
        $marks = array("70", "63", null, "50", "60");
        $result = validate($modules, $marks);
        $this->assertEquals("", $result);
    }

    /** @test */
    public function fails_with_a_missing_mark_for_a_module()
    {
        $modules = array("CSC 3021", "CSC 3059", "CSC 3063", "CSC 3065", "CSC 3068");
        $marks = array("70", "63", null, "50", "60");
        $result = validate($modules, $marks);
        $this->assertEquals("Please provide a valid integer for every entered module", $result);
    }

    /** @test */
    public function fails_with_a_missing_module_for_a_mark()
    {
        $modules = array("CSC 3021", null, "CSC 3063", "CSC 3065", "CSC 3068");
        $marks = array("70", "63", "70", "50", "60");
        $result = validate($modules, $marks);
        $this->assertEquals("Please provide a module name for all marks entered", $result);
    }

    /** @test */
    public function fails_with_all_modules_missing()
    {
        $modules = array(null, null, null, null, null);
        $marks = array("70", "63", "70", "50", "60");
        $result = validate($modules, $marks);
        $this->assertEquals("Please provide some module codes and their respective marks", $result);
    }

    /** @test */
    public function fails_with_mark_over_100_missing()
    {
        $modules = array("CSC 3021", "CSC 3059", "CSC 3063", "CSC 3065", "CSC 3068");
        $marks = array("70", "63", "70", "101", "60");
        $result = validate($modules, $marks);
        $this->assertEquals("Please provide a valid integer for every entered module", $result);
    }

    /** @test */
    public function fails_with_mark_below_0_missing()
    {
        $modules = array("CSC 3021", "CSC 3059", "CSC 3063", "CSC 3065", "CSC 3068");
        $marks = array("70", "63", "70", "-1", "60");
        $result = validate($modules, $marks);
        $this->assertEquals("Please provide a valid integer for every entered module", $result);
    }
}
