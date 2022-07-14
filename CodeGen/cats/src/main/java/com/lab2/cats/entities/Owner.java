package com.lab2.cats.entities;

import javax.persistence.*;
import java.io.Serializable;
import java.time.LocalDate;
import java.util.ArrayList;
import java.util.List;

@Entity
@Table(name = "owners")
public class Owner implements Serializable {
    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    @Column(name = "id")
    private Long id;
    @Column(name="name")
    private String name;
    @Column(name ="date_of_birth")
    private LocalDate date_of_birth;
    @OneToMany(mappedBy = "owner_id", fetch = FetchType.EAGER)
    private List<Cat> cats;

    public Owner(String name, LocalDate dateOfBirth, List<Cat> cats) {
        this.name = name;
        this.date_of_birth = dateOfBirth;
        this.cats = cats;
    }

    public Owner(){

    }

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getName() {
        return name;
    }

    public void setDate_of_birth(LocalDate dateOfBirth) {
        this.date_of_birth = dateOfBirth;
    }

    public LocalDate getDate_of_birth() {
        return date_of_birth;
    }

    public void setCats(List<Cat> cats) {
        this.cats = cats;
    }

    public List<Cat> getCats() {
        return cats;
    }
}
