package com.company;
import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;

public class Main7 {
    public static void main(String[] args) {
        ObjectMapper objectMapper = new ObjectMapper();

        TestObject userObject = new TestObject("Test", 21);
        String testObjectAsJson = null;
        try {
            testObjectAsJson = objectMapper.writeValueAsString(userObject);
        } catch (JsonProcessingException e) {
            e.printStackTrace();
        }

        System.out.println(testObjectAsJson);
    }
}
