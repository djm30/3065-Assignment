package main

import (
	"reflect"
	"testing"
)

func Test_Total_Returns_Expected_200(t *testing.T) {
	result := totalMarks([]int{20, 30, 40, 50, 60})
	want := 200

	if result != want {
		t.Errorf("got %q, wanted %q", result, want)
	}
}

func Test_Total_Returns_All_Zeros_For_Array_Of_Zeros(t *testing.T) {
	result := totalMarks([]int{0, 0, 0, 0, 0})
	want := 0

	if result != want {
		t.Errorf("got %q, wanted %q", result, want)
	}
}

func Test_StringMarks_ToInt_Returns_Int_Of_Strings(t *testing.T) {
	marks := [5]string{"80", "70", "70", "80", "60"}
	result := markStringToInt(marks[:])
	want := [5]int{80, 70, 70, 80, 60}
	if !reflect.DeepEqual(result[:], want[:]) {
		t.Errorf("got %q, wanted %q", result, want)
	}
}

func Test_StringMarks_ToInt_Replaces_Empty_With_0(t *testing.T) {
	marks := [5]string{"80", "70", "70", "80", ""}
	result := markStringToInt(marks[:])
	want := [5]int{80, 70, 70, 80, 0}
	if !reflect.DeepEqual(result[:], want[:]) {
		t.Errorf("got %q, wanted %q", result, want)
	}
}

func Test_StringMarks_ToInt_Replaces_All_Empty_With_0(t *testing.T) {
	marks := [5]string{"", "", "", "", " "}
	result := markStringToInt(marks[:])
	want := [5]int{0, 0, 0, 0, 0}
	if !reflect.DeepEqual(result[:], want[:]) {
		t.Errorf("got %q, wanted %q", result, want)
	}
}

func Test_StringMarks_ToInt_Replaces_All_Invalid_With_0(t *testing.T) {
	marks := [5]string{"80", "70", "notastring", "", " "}
	result := markStringToInt(marks[:])
	want := [5]int{80, 70, 0, 0, 0}
	if !reflect.DeepEqual(result[:], want[:]) {
		t.Errorf("got %q, wanted %q", result, want)
	}
}

func Test_StringMarks_ToInt_Replaces_Over_100_With_0(t *testing.T) {
	marks := [5]string{"80", "70", "70", "0", "101"}
	result := markStringToInt(marks[:])
	want := [5]int{80, 70, 70, 0, 0}
	if !reflect.DeepEqual(result[:], want[:]) {
		t.Errorf("got %q, wanted %q", result, want)
	}
}

func Test_StringMarks_ToInt_Replaces_Below_0_With_0(t *testing.T) {
	marks := [5]string{"80", "70", "70", "100", "-1"}
	result := markStringToInt(marks[:])
	want := [5]int{80, 70, 70, 100, 0}
	if !reflect.DeepEqual(result[:], want[:]) {
		t.Errorf("got %q, wanted %q", result, want)
	}
}
