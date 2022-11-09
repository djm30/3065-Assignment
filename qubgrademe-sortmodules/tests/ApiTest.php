<?php

use \PHPUnit\Framework\TestCase;
use GuzzleHttp\Client;

class TestSortAPITest extends TestCase
{
    private $http;

    protected function setUp(): void
    {
        $this->http = new Client(["base_uri" => "http://localhost:9001/"]);
    }

    protected function tearDown(): void
    {
        $this->http = null;
    }

    /** @test */
    public function api_returns_200_with_valid_params()
    {
        $response = $this->http->request("GET", "?module_1=One&module_2=Two&module_3=Three&module_4=Four&module_5=Five&mark_1=68&mark_2=72&mark_3=62&mark_4=70&mark_5=60");
        $this->assertEquals(200, $response->getStatusCode());
        $responseData = json_decode($response->getBody(), true);
        echo print_r($responseData);
        $this->assertEquals(false, $responseData["error"]);
        $this->assertEquals("", $responseData["errorMessage"]);
        $this->assertEquals(array("One", "Two", "Three", "Four", "Five"), $responseData["modules"]);
        $this->assertEquals(array(68, 72, 62, 70, 60), $responseData["marks"]);
        $this->assertEquals(array(
            array(
                "module" => "Two",
                "marks" => 72
            ),
            array(
                "module" => "Four",
                "marks" => 70
            ),
            array(
                "module" => "One",
                "marks" => 68
            ),
            array(
                "module" => "Three",
                "marks" => 62
            ),
            array(
                "module" => "Five",
                "marks" => 60
            ),
        ), $responseData["sorted_modules"]);
    }

    /** @test */
    public function api_returns_400_with_invalid_params()
    {
        $this->expectException(GuzzleHttp\Exception\ClientException::class);
        $response = @$this->http->request("GET", "?module_1=One&module_2=Two&module_3=Three&module_4=Four&module_5=Five&mark_1=68&mark_2=72&mark_3=62&mark_4=70&mark_5=");
        $this->assertEquals(400, $response->getStatusCode());
        $responseData = json_decode($response->getBody(), true);
        echo print_r($responseData);
        $this->assertEquals(true, $responseData["error"]);
        $this->assertEquals("Please provide a valid integer for every entered module", $responseData["errorMessage"]);
        $this->assertEquals(array("One", "Two", "Three", "Four", "Five"), $responseData["modules"]);
        $this->assertEquals(array(68, 72, 62, 70, 0), $responseData["marks"]);
        $this->assertEquals("", $responseData["sorted_modules"]);
    }
}

class TestHealthCheckAPI extends TestCase
{
    private $http;

    protected function setUp(): void
    {
        $this->http = new Client(["base_uri" => "http://localhost:9001/health.php"]);
    }

    protected function tearDown(): void
    {
        $this->http = null;
    }

    /** @test */
    public function health_check_returns_200()
    {
        $response = $this->http->request("GET");
        $this->assertEquals(200, $response->getStatusCode());
        $this->assertEquals(200, $response->getStatusCode());
        $responseData = json_decode($response->getBody(), true);
        $this->assertNotNull($responseData["message"]);
        $this->assertNotNull($responseData["uptime"]);
        $this->assertNotNull($responseData["date"]);
    }
}
