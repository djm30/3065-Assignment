package main

import (
	"net/http"
	"time"

	"github.com/gin-gonic/gin"

	"fmt"
)

var startTime time.Time

func uptime() time.Duration {
	return time.Since(startTime)
}

func setupRouter() *gin.Engine {
	r := gin.Default()

	r.GET("/health", health)
	r.GET("/", total)
	return r
}

func main() {
	r := setupRouter()
	r.Run()
}

func health(c *gin.Context) {
	c.JSON(http.StatusOK, gin.H{
		"message": "Ok",
		"date":    time.Now(),
		"uptime":  uptime().String(),
	})
}

func total(c *gin.Context) {

	module_1 := c.DefaultQuery("module_1", "")
	module_2 := c.DefaultQuery("module_2", "")
	module_3 := c.DefaultQuery("module_3", "")
	module_4 := c.DefaultQuery("module_4", "")
	module_5 := c.DefaultQuery("module_5", "")

	mark_1 := c.DefaultQuery("mark_1", "")
	mark_2 := c.DefaultQuery("mark_2", "")
	mark_3 := c.DefaultQuery("mark_3", "")
	mark_4 := c.DefaultQuery("mark_4", "")
	mark_5 := c.DefaultQuery("mark_5", "")

	marks := [5]string{mark_1, mark_2, mark_3, mark_4, mark_5}
	modules := [5]string{module_1, module_2, module_3, module_4, module_5}

	statusCode := http.StatusOK
	error := false
	errorMessage := validate(modules[:], marks[:])
	intMarks := markStringToInt(marks[:])
	total := 0

	if errorMessage == "" {
		total = totalMarks(intMarks)
	} else {
		error = true
		statusCode = http.StatusBadRequest
	}

	fmt.Println(marks)
	fmt.Println(modules)

	c.JSON(statusCode, gin.H{
		"error":        error,
		"errorMessage": errorMessage,
		"modules":      modules,
		"marks":        intMarks,
		"total":        total,
	})
}
