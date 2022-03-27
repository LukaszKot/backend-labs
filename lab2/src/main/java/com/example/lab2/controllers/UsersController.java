package com.example.lab2.controllers;

import com.example.lab2.entitities.UserEntity;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseBody;

import java.util.Collection;
import java.util.HashMap;
import java.util.Map;
import java.util.Set;

@Controller
public class UsersController {
    private final Map<Integer, UserEntity> _users = new HashMap<>();
    private Integer currentMaxIndex = 0;

    @RequestMapping("/users")
    @ResponseBody
    public Object getUsers() {
        return _users;
    }

    @RequestMapping("/users/{id}/get")
    @ResponseBody
    public Object getUser(@PathVariable Integer id) {
        return _users.get(id);
    }

    @RequestMapping("/users/add")
    @ResponseBody
    public String addUser(@RequestParam String name, int age) {
        currentMaxIndex++;
        _users.put(currentMaxIndex, new UserEntity(currentMaxIndex, name, age));
        return "";
    }

    @RequestMapping("/users/{id}/remove")
    @ResponseBody
    public String removeUser(@PathVariable Integer id) {
        _users.remove(id);
        return "";
    }
}