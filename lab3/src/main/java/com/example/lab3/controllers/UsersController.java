package com.example.lab3.controllers;

import com.example.lab3.commands.CreateUser;
import com.example.lab3.commands.UpdateUser;
import com.example.lab3.dtos.PaginatedResult;
import com.example.lab3.dtos.Result;
import com.example.lab3.models.UserEntity;
import com.example.lab3.services.UserService;
import org.springframework.http.MediaType;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

@Controller
public class UsersController {

    private final UserService userService;

    public UsersController(UserService userService) {
        this.userService = userService;
    }

    @RequestMapping(
            value = "/api/user/create",
            method = RequestMethod.POST,
            consumes = MediaType.APPLICATION_JSON_VALUE,
            produces = MediaType.APPLICATION_JSON_VALUE
    )
    @ResponseBody
    public UserEntity createUser(@RequestBody CreateUser command) {
        return userService.create(command);
    }

    @RequestMapping(
            value = "/api/users/{id}",
            method = RequestMethod.GET,
            consumes = MediaType.APPLICATION_JSON_VALUE,
            produces = MediaType.APPLICATION_JSON_VALUE
    )
    @ResponseBody
    public UserEntity getUser(@PathVariable Integer id) {
        return userService.get(id);
    }

    @RequestMapping(
            value = "/api/users/{id}/update",
            method = RequestMethod.POST,
            consumes = MediaType.APPLICATION_JSON_VALUE,
            produces = MediaType.APPLICATION_JSON_VALUE
    )
    @ResponseBody
    public UserEntity updateUser(@PathVariable Integer id, @RequestBody UpdateUser command) {
        return userService.update(id, command);
    }

    @RequestMapping(
            value = "/api/users/{id}/remove",
            method = RequestMethod.DELETE,
            consumes = MediaType.APPLICATION_JSON_VALUE,
            produces = MediaType.APPLICATION_JSON_VALUE
    )
    @ResponseBody
    public Result removeUser(@PathVariable Integer id) {
        return new Result(userService.delete(id));
    }

    @RequestMapping(
            value = "/api/users",
            method = RequestMethod.GET,
            consumes = MediaType.APPLICATION_JSON_VALUE,
            produces = MediaType.APPLICATION_JSON_VALUE
    )
    @ResponseBody
    public PaginatedResult<UserEntity> getUsers(@RequestParam(value = "page-number", required = false, defaultValue = "1") Integer pageNumber,
                                    @RequestParam(value = "page-size", required = false, defaultValue = "10") Integer pageSize) {
        return userService.get(pageNumber, pageSize);
    }
}
