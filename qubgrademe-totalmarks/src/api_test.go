package main

import (
	"net/http"
	"net/http/httptest"
	"strings"
	"testing"

	"github.com/stretchr/testify/assert"
)

func Test_Health_Route(t *testing.T) {
	router := setupRouter()

	w := httptest.NewRecorder()
	req, _ := http.NewRequest("GET", "/health", nil)
	router.ServeHTTP(w, req)

	assert.Equal(t, 200, w.Code)
	assert.True(t, strings.Contains(w.Body.String(), "\"message\":\"Ok\""))
	assert.True(t, strings.Contains(w.Body.String(), "date"))
	assert.True(t, strings.Contains(w.Body.String(), "uptime"))
}

func Test_Total_Route_With_Valid_Values(t *testing.T) {
	router := setupRouter()

	w := httptest.NewRecorder()
	req, _ := http.NewRequest("GET", "/?module_1=One&module_2=Two&module_3=Three&module_4=Four&module_5=Five&mark_1=65&mark_2=65&mark_3=65&mark_4=65&mark_5=65", nil)
	router.ServeHTTP(w, req)

	want := "{\"error\":false,\"errorMessage\":\"\",\"marks\":[65,65,65,65,65],\"modules\":[\"One\",\"Two\",\"Three\",\"Four\",\"Five\"],\"total\":325}"

	assert.Equal(t, 200, w.Code)
	assert.Equal(t, want, w.Body.String())
}

func Test_Total_Route_With_Invalid_Values(t *testing.T) {
	router := setupRouter()

	w := httptest.NewRecorder()
	req, _ := http.NewRequest("GET", "/?module_1=One&module_2=Two&module_3=Three&module_4=Four&module_5=Five&mark_1=65&mark_2=65&mark_3=65&mark_4=65&mark_5=", nil)
	router.ServeHTTP(w, req)

	want := "{\"error\":true,\"errorMessage\":\"Please provide a valid integer for every entered module\",\"marks\":[65,65,65,65,0],\"modules\":[\"One\",\"Two\",\"Three\",\"Four\",\"Five\"],\"total\":0}"

	assert.Equal(t, 400, w.Code)
	assert.Equal(t, want, w.Body.String())
}

func Test_Total_Route_With_Invalid_No_Query_String(t *testing.T) {
	router := setupRouter()

	w := httptest.NewRecorder()
	req, _ := http.NewRequest("GET", "/", nil)
	router.ServeHTTP(w, req)

	want := "{\"error\":true,\"errorMessage\":\"Please provide some module codes and their respective marks\",\"marks\":[0,0,0,0,0],\"modules\":[\"\",\"\",\"\",\"\",\"\"],\"total\":0}"

	assert.Equal(t, 400, w.Code)
	assert.Equal(t, want, w.Body.String())
}
