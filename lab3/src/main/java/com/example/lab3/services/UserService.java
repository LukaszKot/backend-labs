package com.example.lab3.services;

import com.example.lab3.commands.CreateUser;
import com.example.lab3.commands.UpdateUser;
import com.example.lab3.dtos.PaginatedResult;
import com.example.lab3.helpers.FileHelper;
import com.example.lab3.models.UserEntity;
import org.springframework.http.HttpStatus;
import org.springframework.stereotype.Service;
import org.springframework.web.server.ResponseStatusException;

import javax.annotation.PostConstruct;
import javax.annotation.PreDestroy;
import java.util.ArrayList;
import java.util.Comparator;
import java.util.List;
import java.util.NoSuchElementException;
import java.util.stream.Collectors;

@Service
public class UserService {
    private List<UserEntity> entities = new ArrayList<>();
    private Integer currentMaxValue = 0;

    @PostConstruct
    private void onCreate() {
        entities = FileHelper.readFile();
        currentMaxValue = entities.stream().max(Comparator.comparingInt(UserEntity::getId)).get().getId();
    }

    @PreDestroy
    private void onDestroy() {
        FileHelper.writeToFile(entities);
    }

    public UserEntity create(CreateUser command){
        currentMaxValue++;
        var entity = new UserEntity(currentMaxValue, command.name, command.email);
        entities.add(entity);
        return entity;
    }

    public PaginatedResult<UserEntity> get(int pageNumber, int pageSize) throws ResponseStatusException {
        if(pageNumber < 1)
        {
            throw new ResponseStatusException(HttpStatus.BAD_REQUEST, "pageNumber have to be at least one");
        }
        if(pageSize < 1 || pageSize > 100)
        {
            throw new ResponseStatusException(HttpStatus.BAD_REQUEST, "pageSize have to be between 1 and 100");
        }

        List<UserEntity> result = entities
                .stream()
                .skip((long) pageSize *(pageNumber-1))
                .limit(pageSize)
                .collect(Collectors.toList());
        return new PaginatedResult<>(result, pageNumber, entities.size()/pageSize, pageSize, entities.size());

    }

    public UserEntity get(int id) throws ResponseStatusException {
        UserEntity userEntity;
        try
        {
            userEntity = entities.stream()
                    .filter(entity -> entity.getId() == id)
                    .findFirst().get();
        }
        catch (NoSuchElementException ex)
        {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "User with given id has not been found.");
        }
        return userEntity;
    }

    public boolean delete(Integer id) {
        var entity = get(id);
        entities.remove(entity);
        return true;
    }

    public UserEntity update(Integer id, UpdateUser command) {
        var entity = get(id);
        entity.setName(command.name);
        entity.setEmail(command.email);
        return entity;
    }
}
