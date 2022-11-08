from src.validation import validate, is_string_positive_integer

# class Test_is_string_positive_integer():
#     def test_10_string_is_valid(self):
#         result = is_string_positive_integer("10")
#         assert result == True

#     def test_10_int_is_valid(self):
#         result = is_string_positive_integer(10)
#         assert result == True

#     def none_is_not_valid(self):
#         result = is_string_positive_integer(10)
#         assert result == True

#     def test_string_above_100_is_not_valid(self):
#         result = is_string_positive_integer("101")
#         assert result == False

#     def test_number_above_100_is_not_valid(self):
#         result = is_string_positive_integer(101)
#         assert result == False

#     def test_string_below_0_is_not_valid(self):
#         result = is_string_positive_integer("-3")
#         assert result == False

#     def test_number_below_0_is_not_valid(self):
#         result = is_string_positive_integer(-3)
#         assert result == False


class Test_validate():

    # def test_succeeds_with_all_modules_and_valid_marks(self):
    #     modules = [
    #             "CSC 3065",
    #             "CSC3021",
    #             "CSC3063",
    #             "CSC3059",
    #             "CSC3069",
    #         ]
    #     marks = ["80", "70", "70", "80", "60"]

    #     success, message = validate(modules, marks)
    #     assert success == True
    #     assert message == ""

    # def test_succeeds_with_missing_pair_of_modules_and_marks(self):
    #     modules = [
    #             "CSC 3065",
    #             "CSC3021",
    #             "CSC3063",
    #             "CSC3059",
    #             None,
    #         ]
    #     marks = ["80", "70", "70", "80", None]

    #     success, message = validate(modules, marks)
    #     assert success == True
    #     assert message == ""

    #     modules = [
    #             "CSC 3065",
    #             "CSC3021",
    #             "CSC3063",
    #             "CSC3059",
    #             "CSC3069",
    #         ]
    #     marks = ["80", "70", "70", "80", "60"]

    #     success, message = validate(modules, marks)
    #     assert success == True
    #     assert message == ""

    # def test_succeeds_with_missing_pair_of_modules_and_marks2(self):
        # modules = [
        #         None,
        #         "CSC3021",
        #         "CSC3063",
        #         "CSC3059",
        #         None,
        #     ]
        # marks = ["80", "70", "70", "80", None]

        # success, message = validate(modules, marks)
        # assert success == True
        # assert message == ""

        # modules = [
        #         None,
        #         "CSC3021",
        #         "CSC3063",
        #         "CSC3059",
        #         "CSC3069",
        #     ]
        # marks = ["80", "70", "70", "80", "60"]

        # success, message = validate(modules, marks)
        # assert success == True
        # assert message == ""

    def test_fails_with_mark_not_present_for_module(self):
        modules = [
                "CSC 3065",
                "CSC 3021",
                "CSC 3063",
                "CSC 3059",
                "CSC 3069",
            ]
        marks = ["80", "70", "70", "80", None]

        success, message = validate(modules, marks)
        assert success == False
        assert message == "Please provide a valid integer for every entered module" 
    
    def test_fails_with_mark_over_100(self):
        modules = [
                "CSC 3065",
                "CSC 3021",
                "CSC 3063",
                "CSC 3059",
                "CSC 3069",
            ]
        marks = ["80", "70", "70", "80", "120"]

        success, message = validate(modules, marks)
        assert success == False
        assert message == "Please provide a valid integer for every entered module" 

    def test_fails_with_mark_below_0(self):
        modules = [
                "CSC 3065",
                "CSC 3021",
                "CSC 3063",
                "CSC 3059",
                "CSC 3069",
            ]
        marks = ["80", "70", "70", "80", "-1"]

        success, message = validate(modules, marks)
        assert success == False
        assert message == "Please provide a valid integer for every entered module" 

    def test_fails_with_module_not_present_for_mark(self):
        modules = [
                "CSC 3065",
                "CSC 3021",
                "CSC 3063",
                "CSC 3059",
                None,
            ]
        marks = ["80", "70", "70", "80", "30"]

        success, message = validate(modules, marks)
        assert success == False
        assert message == "Please provide a module name for all marks entered" 

    def test_fails_if_no_modules_are_entered(self):
        modules = [
                None,
                None,
                None,
                None,
                None,
            ]
        marks = ["80", "70", "70", "80", "30"]

        success, message = validate(modules, marks)
        assert success == False
        assert message == "Please provide some module codes and their respective marks" 

    def test_fails_if_no_modules_are_entered_empty_string(self):
        modules = [
                "",
                "",
                "",
                "",
                "",
            ]
        marks = ["80", "70", "70", "80", "30"]

        success, message = validate(modules, marks)
        assert success == False
        assert message == "Please provide some module codes and their respective marks" 
    
    def test_fails_if_no_modules_are_entered_whitespace(self):
        modules = [
                " ",
                " ",
                " ",
                " ",
                " ",
            ]
        marks = ["80", "70", "70", "80", "30"]

        success, message = validate(modules, marks)
        assert success == False
        assert message == "Please provide some module codes and their respective marks" 

    def test_fails_mix_cases(self):
        modules = [
                "CSC 3065",
                "CSC 3021",
                "CSC 3063",
                " ",
                "CSC 3069",
            ]
        marks = ["80", "70", "70", "80", None]

        success, message = validate(modules, marks)
        assert success == False
        assert message == "Please provide a module name for all marks entered" 


    def test_fails_mix_cases2(self):
        modules = [
                "CSC 3065",
                "CSC 3021",
                "CSC 3063",
                "CSC 3068",
                " ",
            ]
        marks = ["80", "70", "70", None, "80"]

        success, message = validate(modules, marks)
        assert success == False
        assert message == "Please provide a valid integer for every entered module" 