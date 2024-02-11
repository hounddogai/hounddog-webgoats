package com.ii.app.config.security;

import io.jsonwebtoken.JwtBuilder;
import io.jsonwebtoken.Jwts;
import io.jsonwebtoken.SignatureAlgorithm;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.stereotype.Component;

import javax.crypto.spec.SecretKeySpec;
import javax.xml.bind.DatatypeConverter;
import java.security.Key;
import java.util.Date;
import java.util.List;

public class JwtTokenGenerator {

    private JwtTokenGenerator() {
    }

    @SuppressWarnings("deprecation")
    public static String generate(String username, String identifier,String email, String fullName, String phone,List<String> roles) {
        byte[] apiKeySecretBytes = SecurityConstants.JWT_SECRET.getBytes();
        Key signingKey = new SecretKeySpec(apiKeySecretBytes, SignatureAlgorithm.HS512.getJcaName());

        return Jwts.builder()
            .claim("rol", roles)
            .claim("id",identifier )
            .claim("email",email)
            .claim("fullName",fullName)
            .claim("phone",phone)
            .setSubject(username)
            .setExpiration(new Date(System.currentTimeMillis() + SecurityConstants.TTL_TOKEN))
            .signWith(SignatureAlgorithm.HS512, signingKey)
            .compact();
    }

}
