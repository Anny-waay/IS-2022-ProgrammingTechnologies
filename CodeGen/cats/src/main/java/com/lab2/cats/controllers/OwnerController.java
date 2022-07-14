package com.lab2.cats.controllers;

import com.lab2.cats.entities.Cat;
import com.lab2.cats.entities.Owner;
import com.lab2.cats.services.OwnerService;
import org.springframework.http.MediaType;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.Optional;

@RestController
@RequestMapping("owners")
public class OwnerController {
    private final OwnerService ownerService;

    public OwnerController(OwnerService ownerService) {
        this.ownerService = ownerService;
    }

    @GetMapping(value = "get", produces = MediaType.APPLICATION_JSON_VALUE)
    public Optional<Owner> FindOwnerById(Long id){

        return ownerService.FindOwnerById(id);
    }

    @PostMapping(value = "post")
    public void SaveOwner(@RequestBody Owner owner) {

        ownerService.SaveOwner(owner);
    }
}
