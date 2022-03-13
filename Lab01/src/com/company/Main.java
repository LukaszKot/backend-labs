package com.company;

public class Main {

    public static void main(String[] args) {
        int[] array = {4, 5, 7, 11, 12, 15, 15, 21, 40, 45};
        System.out.println(searchIndex(array, 11));
    }

    public static int searchIndex(int[] array, int value)
    {
        int index = 0;
        var limit = array.length - 1;
        while (index <= limit) {
            double point = Math.ceil((double)(index + limit) / (double)2);
            var entry = array[(int)point];
            if (value > entry) {
                index = (int)point + 1;
                continue;
            }
            if (value < entry) {
                limit = (int)point - 1;
                continue;
            }
            return (int)point;
        }
        return -1;
    }
}