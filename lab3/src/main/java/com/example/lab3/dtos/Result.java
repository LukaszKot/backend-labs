package com.example.lab3.dtos;

public class Result {
    public Result(boolean result)
    {
        this.result = result;
    }

    private boolean result;

    public boolean isResult() {
        return result;
    }

    public void setResult(boolean result) {
        this.result = result;
    }
}
