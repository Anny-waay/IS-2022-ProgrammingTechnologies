package com.lab2.cats.services;

import com.lab2.cats.entities.Owner;
import com.lab2.cats.repositories.OwnerRepository;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;

@Service
public class OwnerService {
    private final OwnerRepository ownerRepository;

    public  OwnerService(OwnerRepository ownerRepository) {

        this.ownerRepository = ownerRepository;
    }

    public Optional<Owner> FindOwnerById(Long id){

        return ownerRepository.findById(id);
    }
    public List<Owner> GetAllOwners(){

        return ownerRepository.findAll();
    }
    public void SaveOwner(Owner owner) {

        ownerRepository.save(owner);
    }
}
