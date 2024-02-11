package com.ii.app.controllers;

import org.springframework.security.access.annotation.Secured;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/api/home")
public class HomeController {
    @RequestMapping(path ="home")
    public String getHome(){
        return "home";
    }

    @RequestMapping(path ="error")
    public String getError(){
        throw new IllegalStateException();
    }

    @RequestMapping(path ="error2")
    public String getError2(){
        throw new RuntimeException("Oops");
    }

    @RequestMapping(path ="authed")
    @Secured({"ROLE_USER", "ROLE_EMPLOYEE"})
    public String getAuthed(){
        return "authed";
    }
}
