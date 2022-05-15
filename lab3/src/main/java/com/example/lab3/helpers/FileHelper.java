package com.example.lab3.helpers;

import com.example.lab3.models.UserEntity;
import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.ObjectMapper;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

public class FileHelper {

    private final static String fileName = "data/data.json";
    public static List<UserEntity> readFile()
    {

        StringBuilder fileContent = new StringBuilder();
        try (FileInputStream stream = new FileInputStream(fileName)) {

            int character;
            while ((character = stream.read()) != -1) {
                fileContent.append((char)character);
            }
        } catch (IOException e) {
            e.printStackTrace();
        }

        ObjectMapper objectMapper = new ObjectMapper();

        List<UserEntity> obj = null;
        try {
            obj = objectMapper.readValue(fileContent.toString(), new TypeReference<>() {
            });
        } catch (JsonProcessingException e) {
            e.printStackTrace();
        }
        return obj;
    }

    public static void writeToFile(List<UserEntity> entities)
    {
        ObjectMapper objectMapper = new ObjectMapper();

        String testObjectAsJson = null;
        try {
            testObjectAsJson = objectMapper.writeValueAsString(entities);
        } catch (JsonProcessingException e) {
            e.printStackTrace();
        }

        File file = new File(fileName);
        if (!file.exists()) {
            try {
                boolean wasCreated = file.createNewFile();
                if(wasCreated) System.out.println("Utworzono plik");
            } catch (IOException e) {
                e.printStackTrace();
            }
        }

        try (FileOutputStream stream = new FileOutputStream(file, false)) {
            assert testObjectAsJson != null;
            byte[] contentInBytes = testObjectAsJson.getBytes();
            stream.write(contentInBytes);
            stream.flush();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}
