module.exports = function (context, req) {
    context.log('GetRewardTypes');

    var rewardTypes = [
        {
            id: 1,
            type: "Attendance",
            name: "Session1 attendance",
            description: "Attendance for session 1(0627/0628)",
            extra: {
                dates: ['062717', '062817']
            }
        },
        {
            id: 2,
            type: "Attendance",
            name: "Session2 attendance",
            description: "Attendance for session 2(0705/0708)",
            extra: {
                dates: ['070517', '070817']
            }

        },
        {
            id: 3,
            type: "Attendance",
            name: "Session3 attendance",
            description: "Attendance for session 3(0718/0719)",
            extra: {
                dates: ['071817', '071917']
            }

        },
        {
            id: 4,
            type: "Attendance",
            name: "Session4 attendance",
            description: "Attendance for session 4(0725/0726)",
            extra: {
                dates: ['072517', '072617']
            }

        },
        {
            id: 5,
            type: "Attendance",
            name: "Session5 attendance",
            description: "Attendance for session 5(0801/0802)",
            extra: {
                dates: ['080117', '080217']
            }

        },
        {
            id: 6,
            type: "Attendance",
            name: "Session6 attendance",
            description: "Attendance for session 6(0808/0809)",
            extra: {
                dates: ['080817', '080917']
            }

        },
        {
            id: 7,
            type: "Attendance",
            name: "Session7 attendance",
            description: "Attendance for session 7(0815/0816)",
            extra: {
                dates: ['081517', '081617']
            }

        },
        {
            id: 8,
            type: "Attendance",
            name: "Session8 attendance",
            description: "Attendance for session 8(0822/0823)",
            extra: {
                dates: ['082217', '082317']
            }

        },
        {
            id: 9,
            type: "Attendance",
            name: "Session9 attendance",
            description: "Attendance for session 8(0829/0830)",
            extra: {
                dates: ['082917', '083017']
            }

        },
        {
            id: 101,
            type: "UpdateProfile",
            name: "Update Profile with GitUrl",
            description: "Updating profile with Giturl"
        },
        {
            id: 201,
            type: "GithubNotes",
            name: "Session1 notes in github",
            description: "Created Session1 notes in github (notes\\session1.txt)",
            extra: {
                filename: ['session1.txt']
            }
        },
        {
            id: 202,
            type: "GithubNotes",
            name: "Session2 notes in github",
            description: "Created Session2 notes in github (notes\\session2.txt)",
            extra: {
                filename: ['session2.txt']
            }
        },
        {
            id: 203,
            type: "GithubNotes",
            name: "Session3 notes in github",
            description: "Created Session3 notes in github (notes\\session3.txt)",
            extra: {
                filename: ['session3.txt']
            }
        },
        {
            id: 204,
            type: "GithubNotes",
            name: "Session4 notes in github",
            description: "Created Session4 notes in github (notes\\session4.txt)",
            extra: {
                filename: ['session4.txt']
            }
        },
        {
            id: 205,
            type: "GithubNotes",
            name: "Session5 notes in github",
            description: "Created Session5 notes in github (notes\\session5.txt)",
            extra: {
                filename: ['session5.txt']
            }
        },
        {
            id: 206,
            type: "GithubNotes",
            name: "Session6 notes in github",
            description: "Created Session6 notes in github (notes\\session6.txt)",
            extra: {
                filename: ['session6.txt']
            }
        },
        {
            id: 207,
            type: "GithubNotes",
            name: "Session7 notes in github",
            description: "Created Session7 notes in github (notes\\session7.txt)",
            extra: {
                filename: ['session7.txt']
            }
        },
        {
            id: 208,
            type: "GithubNotes",
            name: "Session8 notes in github",
            description: "Created Session8 notes in github (notes\\session8.txt)",
            extra: {
                filename: ['session8.txt']
            }
        },
        {
            id: 209,
            type: "GithubNotes",
            name: "Session9 notes in github",
            description: "Created Session9 notes in github (notes\\session9.txt)",
            extra: {
                filename: ['session9.txt']
            }
        },
        {
            id: 301,
            type: "Homework",
            name: "Homework-Conditional-Exercise",
            description: "Conditional exercise home work. (homework\\AgeInfo)",
            extra: {
                directory: ['homework/AgeInfo']
            }
        },
        {
            id: 302,
            type: "Homework",
            name: "Homework-ForLoop-Exercise",
            description: "For loop exercise home work.(homework\\PrintNumbers)",
            extra: {
                directory: ['homework/PrintNumbers']
            }

        },
        {
            id: 303,
            type: "Homework",
            name: "Homework-Conditional-Exercise",
            description: "Conditional exercise home work.(homework\\SchoolInfoApp)",
            extra: {
                directory: ['homework/SchoolInfoApp']
            }
        },
        {
            id: 304,
            type: "Homework",
            name: "Homework-ForLoop-Exercise",
            description: "For loop exercise home work.(homework\\RuntimeCalculator)",
            extra: {
                directory: ['homework/RuntimeCalculator']
            }
        },
        {
            id: 305,
            type: "Homework",
            name: "Homework-DataType-Exercise",
            description: "Creating my own datatype.(homework\\MyHomeWorkDataSample)",
            extra: {
                directory: ['homework/MyHomeWorkDataSample']
            }
        },
        {
            id: 306,
            type: "Homework",
            name: "Homework-Creating methods",
            description: "Creating Methods.(homework\\CarSample)",
            extra: {
                directory: ['homework/CarSample']
            }
        }
        
    ]

    context.res = rewardTypes
    context.done();
};