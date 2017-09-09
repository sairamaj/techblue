module.exports = function (context, req) {

    var parentsWithKids = [
        {
            "id": "41e5af4f-20b5-4c3d-a913-cb7b9006f479",
            "name": "Loyola Pitchai",
            "email": "loyolap@gmail.com",
            "students": [
                {
                    "id": "faeec7c9-4a61-4bb0-bce7-6ada62b04b59",
                    "name": "Sharon",
                    "email": "sharonloyola.06@gmail.com"
                }
            ]
        },
        {
            "id": "240c6413-86f9-48de-8e4b-0a1065860752",
            "name": "Ramesh Sakala",
            "email": "sakala.ramesh@gmail.com",
            "students": [
                {
                    "id": "90212be6-6724-41ef-987e-1cf32197102d",
                    "name": "Akshaya Sakala",
                    "email": "sakala.akshaya@gmail.com"
                }
            ]
        },
        {
            "id": "766e375f-360a-4945-904e-cb4aaac5d1e7",
            "name": "Sairama Jamalapuram",
            "email": "sairamaj@gmail.com",
            "students": [
                {
                    "id": "27a8c58a-4c54-4a39-ad4b-a1aef242d12a",
                    "name": "Sourabh Jamalapuram",
                    "email": "sourabh.jamalapuram@gmail.com"
                }
            ]
        },
        {
            "id": "432708aa-2c80-41f8-bfb7-583b7ef9c879",
            "name": "Uma Adusumilli",
            "kids": null,
            "email": "uma.adusumilli@gmail.com"
        },
        {
            "id": "27a7d9f9-decf-4a2b-8e22-8ae98ba27af5",
            "name": "kirti surya",
            "kids": null,
            "email": "kirtisurya@gmail.com",
            "students": [
                {
                    "id": "95e1fa0f-8df8-41e3-b2ea-e3992af5b638",
                    "name": "Sanya",
                    "email": "forsanuu@gmail.com"
                }
            ]
        },
        {
            "id": "90143805-27e9-483a-9cd3-7c059a274283",
            "name": "Viji",
            "kids": null,
            "email": "vijiashok@gmail.com"
        },
        {
            "id": "abc6bd87-c8f1-44a6-8d38-92e5fda10aea",
            "name": "Radhi Nagabothu",
            "email": "radhika.nagabothu@gmail.com",
            "students": [
                {
                    "id": "dd1efccd-3453-4b28-8a88-a9cd7bb98134",
                    "name": "Sai",
                    "kids": null,
                    "email": "saiteja.nagabothu@gmail.com"
                }
            ]
        },
        {
            "id": "2cbc8d84-5214-449e-9e61-292200867dec",
            "name": "ramesh mantha",
            "email": "r_mantha@hotmail.com",
            "students": [
                {
                    "id": "44a14f20-4c64-4a86-a842-4eed897b9b2f",
                    "name": "NikhilKMantha",
                    "kids": null,
                    "email": "nikhil.k.mantha@gmail.com"
                },
                {
                    "id": "fc7c57f0-117d-4635-9949-acdf38695751",
                    "name": "Sahil Mantha",
                    "kids": null,
                    "email": "sahilmantha2@gmail.com"
                }
            ]
        },
        {
            "id": "f2034f7c-c9ea-4ab9-9d49-12919075e69a",
            "name": "bhargavi pasam",
            "email": "bhargavipdx@gmail.com",
            "students": [
                {
                    "id": "7466bc4f-0140-44d5-bc1a-5c44e881f2d7",
                    "name": "Navyatha B.",
                    "kids": null,
                    "email": "navyatha2012@gmail.com"
                },
                {
                    "id": "7a0ac127-1972-4779-8ab9-350f37407fd6",
                    "name": "Bhavika B",
                    "kids": null,
                    "email": "bsbuddi2014@gmail.com"
                }
            ]
        },
        {
            "id": "c863a4a0-2e45-41e0-a120-d8f9b1a76d5a",
            "name": "Prasad Dharmala",
            "email": "dharmalap@gmail.com",
            "students": [
                {
                    "id": "d703f382-16be-4d00-b808-483a791b7178",
                    "name": "Varun Dharmala",
                    "kids": null,
                    "email": "vdharmala@gmail.com"
                }
            ]
        },
        {
            "id": "81f711fb-9316-4c1b-a347-6af950d16bf9",
            "name": "Krish Surya",
            "email": "krishnasurya@gmail.com",
            "students": [
                {
                    "id": "95e1fa0f-8df8-41e3-b2ea-e3992af5b638",
                    "name": "Sanya",
                    "kids": null,
                    "email": "forsanuu@gmail.com"
                }
            ]
        },
        {
            "id": "709bc826-0472-42da-8b2d-b006bbc7e447",
            "name": "Vijayalaxmi Bukka",
            "kids": null,
            "email": "veeji10@gmail.com"
        },
        {
            "id": "58ff9468-1bbb-4679-9201-eec2e57d4f89",
            "name": "Nagaraj Sathyanarayan",
            "email": "prajwalpranav@gmail.com",
            "students": [
                {
                    "id": "a05925aa-7152-4b97-838b-9d8043c2af00",
                    "name": "Pranav Sharma",
                    "kids": null,
                    "email": "pranavpdx@gmail.com"
                }
            ]
        },
        {
            "id": "1aea65df-e67e-40dd-bc9b-b59cd4309fbf",
            "name": "Anu",
            "email": "anu.narahari@gmail.com",
            "students": [
                {
                    "id": "e766555e-59f9-4d8c-8ae5-0fb5081104ab",
                    "name": "Tarun Narahari",
                    "kids": null,
                    "email": "tarun.narahari@gmail.com"
                }
            ]
        },
        {
            "id": "d7f2ac15-5b10-4d7f-9d6f-053072769232",
            "name": "Srinivasulu Gubba",
            "kids": null,
            "email": "gubbasrini@gmail.com"
        },
        {
            "id": "a0d2621f-eaa0-4ae8-921b-f5c84f942e2a",
            "name": "Suparna Shanmugam",
            "kids": null,
            "email": "suparna.vijaybabu@gmail.com"
        },
        {
            "id": "ba91c236-e283-46eb-9cd3-a7d93e597fcd",
            "name": "Vibha",
            "email": "veebes25@gmail.com",
            "students": [
                {
                    "id": "ee81fab6-2072-41b4-9089-280890896076",
                    "name": "Prisha Velhal",
                    "kids": null,
                    "email": "393893@bsd48.org"
                }
            ]
        },
        {
            "id": "7b304d86-d948-481b-98e9-19feb7a1b2ae",
            "name": "sairamaj@hotmail.com JAMALAPURAM",
            "email": "sairamaj@hotmail.com",
            "students": [
                {
                    "id": "27a8c58a-4c54-4a39-ad4b-a1aef242d12a",
                    "name": "Sourabh Jamalapuram",
                    "email": "sourabh.jamalapuram@gmail.com"
                }
            ]
        },
        {
            "id": "d78591ca-4fa6-41d6-8e6a-e833a12e60a8",
            "name": "Ratna Kishore Jupudi",
            "email": "ratnakishore.jupudi@gmail.com",
            "students": [
                {
                    "id": "0333118c-a4e4-46b1-adcc-390d368b71c5",
                    "name": "Yashasvini",
                    "email": "459185@bsd48.org"
                }
            ]
        }
    ]

    context.res = parentsWithKids
    context.done();
};