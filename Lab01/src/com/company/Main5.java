package com.company;


import java.time.Instant;
import java.time.LocalDateTime;

public class Main5 {

    public static void main(String[] args) {
        System.out.println("UTC "+Instant.now().toString());
        System.out.println("Local "+ LocalDateTime.now());
    }
}

