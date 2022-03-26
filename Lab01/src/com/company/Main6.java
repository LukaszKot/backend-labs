package com.company;


import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;
import java.time.Instant;
import java.time.LocalDateTime;

import static com.company.Main1.readFile;

public class Main6 {

    public static void main(String[] args) {
        String fileContent = readFile("src/com/company/Main5.java");
        String[] lines = fileContent.split(System.lineSeparator());
        for(int i=0; i< lines.length; i++)
        {
            System.out.println((i+1) + "\t" + lines[i]);
        }
    }
}

