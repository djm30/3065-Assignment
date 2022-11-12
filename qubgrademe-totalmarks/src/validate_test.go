package main

import "testing"

// Testing validMark method
func Test_Valid_Mark_Is_True_For_String_100(t *testing.T) {
	result := validMark("100")
	want := true

	if result != want {
		t.Errorf("got %t, wanted %t", result, want)
	}
}

func Test_Valid_Mark_Is_True_For_String_0(t *testing.T) {
	result := validMark("0")
	want := true

	if result != want {
		t.Errorf("got %t, wanted %t", result, want)
	}
}

func Test_Valid_Mark_Is_True_For_String_50(t *testing.T) {
	result := validMark("50")
	want := true

	if result != want {
		t.Errorf("got %t, wanted %t", result, want)
	}
}

func Test_Valid_Mark_Is_False_For_String_101(t *testing.T) {
	result := validMark("101")
	want := false

	if result != want {
		t.Errorf("got %t, wanted %t", result, want)
	}
}

func Test_Valid_Mark_Is_False_For_String_Minus1(t *testing.T) {
	result := validMark("-1")
	want := false

	if result != want {
		t.Errorf("got %t, wanted %t", result, want)
	}
}

func Test_Valid_Mark_Is_False_For_EmptyString(t *testing.T) {
	result := validMark("")
	want := false

	if result != want {
		t.Errorf("got %t, wanted %t", result, want)
	}
}

func Test_Valid_Mark_Is_False_For_Whitespace(t *testing.T) {
	result := validMark(" ")
	want := false

	if result != want {
		t.Errorf("got %t, wanted %t", result, want)
	}
}

func Test_All_Modules_Empty_Is_True_For_Array_Of_Empty_Strings(t *testing.T) {
	modules := [5]string{"", "", "", "", ""}
	result := allModulesEmpty(modules[:])
	want := true

	if result != want {
		t.Errorf("got %t, wanted %t", result, want)
	}
}

func Test_All_Modules_Empty_Is_True_For_Array_Of_Whitespace_Strings(t *testing.T) {
	modules := [5]string{"  ", "  ", " ", "   ", "    "}
	result := allModulesEmpty(modules[:])
	want := true

	if result != want {
		t.Errorf("got %t, wanted %t", result, want)
	}
}

func Test_All_Modules_Empty_Is_False_For_Array_With_1_Valid_String(t *testing.T) {
	modules := [5]string{"Module Here", "", "", "", ""}
	result := allModulesEmpty(modules[:])
	want := false

	if result != want {
		t.Errorf("got %t, wanted %t", result, want)
	}
}

func Test_All_Modules_Empty_Is_False_For_Array_With_All_Valid_String(t *testing.T) {
	modules := [5]string{"Module 1", "Module 2", "Module 3", "Module 4", "Module 5"}
	result := allModulesEmpty(modules[:])
	want := false

	if result != want {
		t.Errorf("got %t, wanted %t", result, want)
	}
}

func Test_Validate_Passes_For_All_Valid_Values(t *testing.T) {
	modules := [5]string{"CSC 3065",
		"CSC3021",
		"CSC3063",
		"CSC3059",
		"CSC3069"}
	marks := [5]string{"80", "70", "70", "80", "60"}

	result := validate(modules[:], marks[:])
	want := ""

	if result != want {
		t.Errorf("got %s, wanted %s", result, want)
	}
}

func Test_Validate_Passes_For_With_A_Missing_Pair_Of_Values(t *testing.T) {
	modules := [5]string{"CSC 3065",
		"CSC3021",
		"CSC3063",
		"CSC3059",
		""}
	marks := [5]string{"80", "70", "70", "80", ""}

	result := validate(modules[:], marks[:])
	want := ""

	if result != want {
		t.Errorf("got %s, wanted %s", result, want)
	}
}

func Test_Validate_Passes_For_With_Two_Missing_Pairs_Of_Values(t *testing.T) {
	modules := [5]string{"CSC 3065",
		"CSC3021",
		"CSC3063",
		"",
		""}
	marks := [5]string{"80", "70", "70", "", ""}

	result := validate(modules[:], marks[:])
	want := ""

	if result != want {
		t.Errorf("got %s, wanted %s", result, want)
	}
}

func Test_Validate_Passes_For_With_Three_Missing_Pairs_Of_Values(t *testing.T) {
	modules := [5]string{"CSC 3065",
		"CSC3021",
		"",
		"",
		""}
	marks := [5]string{"80", "70", "", "", ""}

	result := validate(modules[:], marks[:])
	want := ""

	if result != want {
		t.Errorf("got %s, wanted %s", result, want)
	}
}

func Test_Validate_Passes_For_With_Four_Missing_Pairs_Of_Values(t *testing.T) {
	modules := [5]string{"CSC 3065",
		"",
		"",
		"",
		""}
	marks := [5]string{"80", "", "", "", ""}

	result := validate(modules[:], marks[:])
	want := ""

	if result != want {
		t.Errorf("got %s, wanted %s", result, want)
	}
}

func Test_Validate_Fails_For_No_Values(t *testing.T) {
	modules := [5]string{"",
		"",
		"",
		"",
		""}
	marks := [5]string{"", "", "", "", ""}

	result := validate(modules[:], marks[:])
	want := "Please provide some module codes and their respective marks"

	if result != want {
		t.Errorf("got %s, wanted %s", result, want)
	}
}

func Test_Validate_Fails_For_All_Whitespace_No_Values(t *testing.T) {
	modules := [5]string{" ",
		" ",
		"  ",
		" ",
		" "}
	marks := [5]string{"", "", "", "", ""}

	result := validate(modules[:], marks[:])
	want := "Please provide some module codes and their respective marks"

	if result != want {
		t.Errorf("got %s, wanted %s", result, want)
	}
}

func Test_Validate_Fails_When_A_Module_Is_Missing_A_Mark(t *testing.T) {
	modules := [5]string{"CSC 3065",
		"CSC3021",
		"CSC3063",
		"CSC3059",
		"CSC3069"}
	marks := [5]string{"80", "70", "70", "80", ""}

	result := validate(modules[:], marks[:])
	want := "Please provide a valid integer for every entered module"

	if result != want {
		t.Errorf("got %s, wanted %s", result, want)
	}
}

func Test_Validate_Fails_When_A_Mark_Is_Missing_A_Module(t *testing.T) {
	modules := [5]string{"CSC 3065",
		"CSC3021",
		"CSC3063",
		"CSC3059",
		""}
	marks := [5]string{"80", "70", "70", "80", "60"}

	result := validate(modules[:], marks[:])
	want := "Please provide a module name for all marks entered"

	if result != want {
		t.Errorf("got %s, wanted %s", result, want)
	}
}

func Test_Validate_Fails_When_A_Mark_Is_Over_100(t *testing.T) {
	modules := [5]string{"CSC 3065",
		"CSC3021",
		"CSC3063",
		"CSC3059",
		"CSC3069"}
	marks := [5]string{"80", "70", "70", "80", "101"}

	result := validate(modules[:], marks[:])
	want := "Please provide a valid integer for every entered module"

	if result != want {
		t.Errorf("got %s, wanted %s", result, want)
	}
}

func Test_Validate_Fails_When_A_Mark_Is_Below_0(t *testing.T) {
	modules := [5]string{"CSC 3065",
		"CSC3021",
		"CSC3063",
		"CSC3059",
		"CSC3069"}
	marks := [5]string{"80", "70", "70", "80", "-1"}

	result := validate(modules[:], marks[:])
	want := "Please provide a valid integer for every entered module"

	if result != want {
		t.Errorf("got %s, wanted %s", result, want)
	}
}

func Test_Validate_Fails_When_A_Mark_Is_An_Invalid_String(t *testing.T) {
	modules := [5]string{"CSC 3065",
		"CSC3021",
		"CSC3063",
		"CSC3059",
		"CSC3069"}
	marks := [5]string{"80", "70", "70", "80", "dsaff"}

	result := validate(modules[:], marks[:])
	want := "Please provide a valid integer for every entered module"

	if result != want {
		t.Errorf("got %s, wanted %s", result, want)
	}
}
