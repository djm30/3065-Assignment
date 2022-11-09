<?php
require __DIR__ . "/../src/functions.inc.php";

use \PHPUnit\Framework\TestCase;

class TestSort extends TestCase
{
    /** @test */
    public function sorts_a_list_modules_in_descending_order()
    {
        $modules = array("CSC 3021", "CSC 3059", "CSC 3063", "CSC 3065", "CSC 3068");
        $marks = array(70, 63, 68, 71, 60);
        $expected = array(
            array(
                "module" => "CSC 3065",
                "marks" => 71
            ),
            array(
                "module" => "CSC 3021",
                "marks" => 70
            ),
            array(
                "module" => "CSC 3063",
                "marks" => 68
            ),
            array(
                "module" => "CSC 3059",
                "marks" => 63
            ),
            array(
                "module" => "CSC 3068",
                "marks" => 60
            ),
        );
        $result = getSortedModules($modules, $marks);
        $this->assertEquals($expected, $result);
    }

    /** @test */
    public function sorts_a_list_with_pairs_missing()
    {
        $modules = array("CSC 3021", "CSC 3059", "CSC 3063", "CSC 3065", "");
        $marks = array(70, 63, 68, 71, 0);
        $expected = array(
            array(
                "module" => "CSC 3065",
                "marks" => 71
            ),
            array(
                "module" => "CSC 3021",
                "marks" => 70
            ),
            array(
                "module" => "CSC 3063",
                "marks" => 68
            ),
            array(
                "module" => "CSC 3059",
                "marks" => 63
            ),
            array(
                "module" => "",
                "marks" => 0
            ),
        );
        $result = getSortedModules($modules, $marks);
        $this->assertEquals($expected, $result);
    }

    /** @test */
    public function sorts_a_list_with_two_pairs_missing()
    {
        $modules = array("CSC 3021", "", "CSC 3063", "CSC 3065", "");
        $marks = array(70, 0, 68, 71, 0);
        $expected = array(
            array(
                "module" => "CSC 3065",
                "marks" => 71
            ),
            array(
                "module" => "CSC 3021",
                "marks" => 70
            ),
            array(
                "module" => "CSC 3063",
                "marks" => 68
            ),
            array(
                "module" => "",
                "marks" => 0
            ),
            array(
                "module" => "",
                "marks" => 0
            ),
        );
        $result = getSortedModules($modules, $marks);
        $this->assertEquals($expected, $result);
    }

    /** @test */
    public function sorts_a_list_with_three_pairs_missing()
    {
        $modules = array("CSC 3021", "CSC 3063", "", "", "");
        $marks = array(70, 63, 0, 0, 0);
        $expected = array(
            array(
                "module" => "CSC 3021",
                "marks" => 70
            ),
            array(
                "module" => "CSC 3063",
                "marks" => 63
            ),
            array(
                "module" => "",
                "marks" => 0
            ),
            array(
                "module" => "",
                "marks" => 0
            ),
            array(
                "module" => "",
                "marks" => 0
            ),
        );
        $result = getSortedModules($modules, $marks);
        $this->assertEquals($expected, $result);
    }

    /** @test */
    public function sorts_a_list_with_four_pairs_missing()
    {
        $modules = array("CSC 3021", "", "", "", "");
        $marks = array(70, 0, 0, 0, 0);
        $expected = array(
            array(
                "module" => "CSC 3021",
                "marks" => 70
            ),
            array(
                "module" => "",
                "marks" => 0
            ),
            array(
                "module" => "",
                "marks" => 0
            ),
            array(
                "module" => "",
                "marks" => 0
            ),
            array(
                "module" => "",
                "marks" => 0
            ),
        );
        $result = getSortedModules($modules, $marks);
        $this->assertEquals($expected, $result);
    }
}
