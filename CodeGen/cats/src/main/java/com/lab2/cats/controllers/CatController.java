package com.lab2.cats.controllers;

import com.lab2.cats.entities.Cat;
import com.lab2.cats.services.CatService;
import org.springframework.http.MediaType;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.Optional;

@RestController
@RequestMapping("cats")
public class CatController {
    private final CatService catService;

    public CatController(CatService catService) {

        this.catService = catService;
    }

    @GetMapping(value = "get", produces = MediaType.APPLICATION_JSON_VALUE)
    public Optional<Cat> FindCatById(Long id) {

        return catService.FindCatById(id);
    }

    @GetMapping(value = "getAll", produces = MediaType.APPLICATION_JSON_VALUE)
    public List<Cat> GetAllCats() {

        return catService.GetAllCats();
    }

    @PostMapping(value = "post")
    public void SaveCat(@RequestBody Cat cat) {

        catService.SaveCat(cat);
    }
}
