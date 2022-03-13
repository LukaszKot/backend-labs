package com.company;


public class Main4 {

    public static final int[] CRC_TABLE = createCrcTable();
    public static void main(String[] args) {
        System.out.println(crc32("This is example text ..."));
    }

    public static int crc32(String text) {
        int crc = -1;
        for (int i = 0; i < text.length(); ++i) {
            int code = text.charAt(i);
            crc = CRC_TABLE[(code ^ crc) & 0xFF] ^ (crc >>> 8);
        }
        return (~crc);
    }

    public static int[] createCrcTable() {
        int[] crcTable = new int[256];

        for (int i = 0; i < 256; ++i) {
            int code = i;
            for (int j = 0; j < 8; ++j) {
                code = (code % 2 != 0 ? 0xEDB88320 ^ (code >>> 1) : (code >>> 1));
            }
            crcTable[i] = code;
        }
        return crcTable;
    }
}

