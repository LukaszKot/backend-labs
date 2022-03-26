package com.company;
import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;

public class Main8 {
    public static void main(String[] args) {
        ObjectMapper objectMapper = new ObjectMapper();

        String str = "{\"name\":\"Test\",\"value\":21}";
        TestObject obj = null;
        try {
            obj = objectMapper.readValue(str, TestObject.class);
        } catch (JsonProcessingException e) {
            e.printStackTrace();
        }

        if(obj != null)
        {
            System.out.println(obj.getName());
            System.out.println(obj.getValue());
        }

    }
}
