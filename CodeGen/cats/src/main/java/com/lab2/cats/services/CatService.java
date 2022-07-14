package com.lab2.cats.services;

import com.lab2.cats.entities.Cat;
import com.lab2.cats.repositories.CatRepository;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;

@Service
public class CatService {

    private final CatRepository catRepository;

    public CatService(CatRepository catRepository) {

        this.catRepository = catRepository;
    }

    public Optional<Cat> FindCatById(Long id){

        return catRepository.findById(id);
    }
    public Cat SaveCat(Cat cat) {

        return catRepository.save(cat);
    }
    public List<Cat> GetAllCats(){

        return catRepository.findAll();
    }
}
