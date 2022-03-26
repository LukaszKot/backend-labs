package com.company;

import java.io.FileInputStream;
import java.io.IOException;

public class Main1 {
    public static void main(String[] args) {
        String fileContent = readFile("src/com/company/Main5.java");
        System.out.println(fileContent);
    }

    public static String readFile(String fileName) {
        StringBuilder fileContent = new StringBuilder();
        try (FileInputStream stream = new FileInputStream(fileName)) {

            int character;
            while ((character = stream.read()) != -1) {
                fileContent.append((char)character);
            }
        } catch (IOException e) {
            e.printStackTrace();
        }
        return fileContent.toString();
    }
}
