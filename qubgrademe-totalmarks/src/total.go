package main

import (
	"strconv"
)

func markStringToInt(marks []string) []int {
	var intMarks [5]int

	for i := 0; i < 5; i++ {
		intMark := 0
		if validMark(marks[i]) {
			m, _ := strconv.Atoi(marks[i])
			intMark = m
		}
		intMarks[i] = intMark
	}
	return intMarks[:]
}

func totalMarks(marks []int) int {
	sum := 0
	for _, v := range marks {
		sum += v
	}
	return sum
}
