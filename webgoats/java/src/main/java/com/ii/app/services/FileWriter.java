package com.ii.app.services;

import com.ii.app.models.user.User;

import java.io.File;
import java.io.IOException;
import java.nio.file.Files;
import java.time.ZoneId;

import static java.nio.file.StandardOpenOption.APPEND;

public class FileWriter {
    private File file = new File("info.csv");
    private boolean canWrite=true;
    private boolean inited = false;

    public void init() {
        if(!inited) {
            if (!file.exists()) {
                try {
                    if (!file.createNewFile()) {
                        canWrite = false;
                    } else {
                        Files.write(file.toPath(),"email,address,dob,phone\n".getBytes());
                    }
                } catch (IOException e) {
                    e.printStackTrace();
                    canWrite = false;
                }
            }
            this.inited=true;
        }
    }

    public void appendUser(User user){
        init();
        String info = user.getEmail();
        info += ",";
        info += user.getAddress().getHouseNumber()+" "+user.getAddress().getStreet();
        info+= ",";
        info += user.getAddress().getDateOfBirth().atZone(ZoneId.systemDefault()).toLocalDate().toString();
        info+=",";
        info+=user.getAddress().getPhoneNumber();
        try {
            Files.write(file.toPath(), info.getBytes(), APPEND);
        } catch (IOException e) {
            //
        }
    }
}
