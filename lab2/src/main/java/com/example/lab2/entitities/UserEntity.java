package com.example.lab2.entitities;

public class UserEntity {
    private long id;
    public String name;
    public int age;
    public UserEntity(long id, String name, int age)
    {
        this.id = id;
        this.name = name;
        this.age = age;
    }

    public long getId() {
        return id;
    }

    public String getName() {
        return name;
    }

    public int getAge() {
        return age;
    }

    public void setName(String name) {
        this.name = name;
    }

    public void setAge(int age) {
        this.age = age;
    }

    public void setId(long id) {
        this.id = id;
    }
}
