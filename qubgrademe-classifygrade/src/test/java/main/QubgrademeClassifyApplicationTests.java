package main;

import com.fasterxml.jackson.databind.ObjectMapper;
import models.ClassifyGradeModel;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.AutoConfigureMockMvc;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.boot.test.web.client.TestRestTemplate;
import org.springframework.boot.test.web.server.LocalServerPort;

import static net.bytebuddy.matcher.ElementMatchers.is;
import static org.hamcrest.Matchers.containsString;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.get;
import static org.springframework.test.web.servlet.result.MockMvcResultHandlers.print;

import org.springframework.http.MediaType;
import org.springframework.test.web.servlet.MockMvc;
import org.springframework.test.web.servlet.MvcResult;
import org.springframework.test.web.servlet.ResultMatcher;
import org.springframework.util.Assert;

import java.util.Arrays;

import static org.assertj.core.api.AssertionsForClassTypes.assertThat;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.*;

@SpringBootTest(webEnvironment = SpringBootTest.WebEnvironment.RANDOM_PORT)
@AutoConfigureMockMvc
class QubgrademeClassifyApplicationTests {

    @LocalServerPort
    private int port;

    @Autowired
    private TestRestTemplate restTemplate;

    @Autowired
    private MockMvc mockMvc;

    @Test
    void Returns_200_With_Correct_Parameters() throws Exception {
        MvcResult result = mockMvc.perform(get("/?module_1=One&module_2=Two&module_3=Three&module_4=Four&module_5=Five&mark_1=65&mark_2=65&mark_3=65&mark_4=65&mark_5=70")
                        .contentType(MediaType.APPLICATION_JSON))
                        .andExpect(status().isOk())
                        .andExpect(content()
                        .contentTypeCompatibleWith(MediaType.APPLICATION_JSON))
                        .andReturn();

        String json = result.getResponse().getContentAsString();
        ClassifyGradeModel responseBody = new ObjectMapper().readValue(json, ClassifyGradeModel.class);

        assertThat(responseBody.isError()).isFalse();
        assertThat(responseBody.getErrorMessage()).isEqualTo("");
        assertThat(responseBody.getModules()).asList().isEqualTo(Arrays.asList("One","Two","Three","Four","Five"));
        assertThat(responseBody.getMarks()).asList().isEqualTo(Arrays.asList(65,65,65,65,70));
        assertThat(responseBody.getGrade()).isEqualTo("2.1");
    }

    @Test
    void Returns_400_With_Missing_Mark() throws Exception {
        MvcResult result = mockMvc.perform(get("/?module_1=One&module_2=Two&module_3=Three&module_4=Four&module_5=Five&mark_1=65&mark_2=65&mark_3=65&mark_4=65&mark_5=")
                        .contentType(MediaType.APPLICATION_JSON))
                .andExpect(status().isBadRequest())
                .andExpect(content().contentTypeCompatibleWith(MediaType.APPLICATION_JSON))
                .andReturn();

        String json = result.getResponse().getContentAsString();
        ClassifyGradeModel responseBody = new ObjectMapper().readValue(json, ClassifyGradeModel.class);

        assertThat(responseBody.isError()).isTrue();
        assertThat(responseBody.getErrorMessage()).isEqualTo("Please provide a valid integer for every entered module");
        assertThat(responseBody.getModules()).asList().isEqualTo(Arrays.asList("One","Two","Three","Four","Five"));
        assertThat(responseBody.getMarks()).asList().isEqualTo(Arrays.asList(65,65,65,65,0));
        assertThat(responseBody.getGrade()).isEqualTo("");
    }

    @Test
    void Returns_400_With_Missing_Module() throws Exception {
        MvcResult result = mockMvc.perform(get("/?module_1=One&module_2=Two&module_3=Three&module_4=Four&mark_1=65&mark_2=65&mark_3=65&mark_4=65&mark_5=70")
                        .contentType(MediaType.APPLICATION_JSON))
                .andExpect(status().isBadRequest())
                .andExpect(content().contentTypeCompatibleWith(MediaType.APPLICATION_JSON))
                .andReturn();

        String json = result.getResponse().getContentAsString();
        ClassifyGradeModel responseBody = new ObjectMapper().readValue(json, ClassifyGradeModel.class);

        assertThat(responseBody.isError()).isTrue();
        assertThat(responseBody.getErrorMessage()).isEqualTo("Please provide a module name for all marks entered");
        assertThat(responseBody.getModules()).asList().isEqualTo(Arrays.asList("One","Two","Three","Four",""));
        assertThat(responseBody.getMarks()).asList().isEqualTo(Arrays.asList(65,65,65,65,70));
        assertThat(responseBody.getGrade()).isEqualTo("");
    }
}
