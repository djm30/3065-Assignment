package main

import (
	"strconv"
	"strings"
)

func validate(modules []string, marks []string) string {

	// Check all modules are empty
	if allModulesEmpty(modules) {
		return "Please provide some module codes and their respective marks"
	}

	// Foreach module, check if there is a valid mark
	for i := 0; i < len(modules); i++ {
		if strings.TrimSpace(modules[i]) != "" {
			// Check if mark at location is valid
			if !validMark(marks[i]) {
				return "Please provide a valid integer for every entered module"
			}
		} else {
			// If not a valid module, ensure there isnt a valid mark here
			if validMark(marks[i]) {
				return "Please provide a module name for all marks entered"
			}
		}
	}
	return ""
}

func allModulesEmpty(modules []string) bool {
	for i := 0; i < len(modules); i++ {
		if strings.TrimSpace(modules[i]) != "" {
			return false
		}
	}
	return true
}

func validMark(mark string) bool {

	if mark, err := strconv.ParseInt(mark, 10, 32); err == nil {
		if mark <= 100 && mark >= 0 {
			return true
		}
	}
	return false
}
