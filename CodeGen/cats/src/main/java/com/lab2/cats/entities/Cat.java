package com.lab2.cats.entities;
import javax.persistence.*;
import java.io.Serializable;
import java.time.LocalDate;

@Entity
@Table(name = "cats")
public class Cat implements Serializable {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id")
    private Long id;
    @Column(name="name")
    private String name;
    @Column(name ="date_of_birth")
    private LocalDate date_of_birth;
    @Column(name="breed")
    private String breed;
    @Column(name="color")
    private String color;
    @Column(name="owner_id")
    private Long owner_id;

    public Cat(String name, LocalDate date_of_birth, String breed, String color, Long owner_id){
        this.name = name;
        this.date_of_birth = date_of_birth;
        this.breed = breed;
        this.color = color;
        this.owner_id = owner_id;
    }

    public Cat(){
    }

    public void setId(Long id) {
        this.id = id;
    }

    public Long getId() {
        return id;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getName() {
        return name;
    }

    public void setDate_of_birth(LocalDate date) {
        this.date_of_birth = date;
    }

    public LocalDate getDate_of_birth() {
        return date_of_birth;
    }

    public void setBreed(String breed) {
        this.breed = breed;
    }

    public String getBreed() {
        return breed;
    }

    public void setColor(String color) {
        this.color = color;
    }

    public String getColor() {
        return color;
    }

    public Long getOwner_id() {
        return owner_id;
    }

    public void setOwner_id(Long owner_id) {
        this.owner_id = owner_id;
    }
}
