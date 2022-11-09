<?php
require __DIR__ . "/../src/functions.inc.php";
require_once __DIR__ . "/../src/validation.inc.php";

use \PHPUnit\Framework\TestCase;

class TestMaxMin extends TestCase
{
    /** @test */
    public function returns_the_max_min_on_values()
    {
        $modules = array("CSC 3021", "CSC 3059", "CSC 3063", "CSC 3065", "CSC 3068");
        $marks = array("70", "63", "68", "71", "60");

        $expected = array("CSC 3065 - 71", "CSC 3068 - 60");
        $result = getMaxMin($modules, $marks);

        $this->assertEquals($expected, $result);
    }

    // /** @test */
    public function returns_the_max_min_on_values_with_a_missing_pair()
    {
        $modules = array("CSC 3021", "CSC 3059", "CSC 3063", "CSC 3065", "");
        $marks = array(70, 63, 68, 71, null);

        $expected = array("CSC 3065 - 71", "CSC 3059 - 63");
        $result = getMaxMin($modules, $marks);

        $this->assertEquals($expected, $result);
    }

    // /** @test */
    public function returns_the_max_min_on_values_with_two_missing_pairs()
    {
        $modules = array("CSC 3021", "CSC 3059", "CSC 3063", "", "");
        $marks = array(70, 63, 68, null, null);

        $expected = array("CSC 3021 - 70", "CSC 3059 - 63");
        $result = getMaxMin($modules, $marks);

        $this->assertEquals($expected, $result);
    }

    /** @test */
    public function returns_the_max_min_on_values_with_three_missing_pairs()
    {
        $modules = array("CSC 3021", "", "CSC 3063", "", "");
        $marks = array(70, null, 68, null, null);

        $expected = array("CSC 3021 - 70", "CSC 3063 - 68");
        $result = getMaxMin($modules, $marks);

        $this->assertEquals($expected, $result);
    }

    /** @test */
    public function returns_the_max_min_on_values_with_four_missing_pairs()
    {
        $modules = array("CSC 3021", "", "", "", "");
        $marks = array(70, null, null, null, null);

        $expected = array("CSC 3021 - 70", "CSC 3021 - 70");
        $result = getMaxMin($modules, $marks);

        $this->assertEquals($expected, $result);
    }
}
