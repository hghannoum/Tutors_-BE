using DALC;
using BLC;
using Enteties;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
namespace tutorsleb.Controllers
{
    [Route("api")]
    [ApiController]
    public class tutorsleb : ControllerBase
    {
        [HttpPost]
        [Route("data")]
        public response data(data dt)
        {
            return DALC.DALC.refreshData(dt.email);
        }
        [HttpPost]
        [Route("relations")]
        public response[] relations(object obj )
        {
            return DALC.DALC.selectCon(obj.ToString());
        }
        [HttpPost]
        [Route("getMeetings")]
        public response getMeetings(data dt)
        {
            return DALC.DALC.getMeetings(dt.email);
        }
        [HttpPost]
        [Route("getStudent")]
        public response getStudent(object obj)
        {
            return DALC.DALC.getStudents(obj.ToString());

        }
        [HttpPost]
        [Route("filter")]
        public DataTable filter(filterObj filter)
        {
            return BLC.BLC.filter(filter.arr,filter.dt);

        }
        [Route("emails")]
        public response emails()
        {
            return DALC.DALC.refreshEmails();
        }
        [HttpPost]
        [Route("userExist")]
        public bool userExist(validation o)
        {
            return BLC.BLC.IsUserExist(o.email,o.data);
        }
        [HttpPost]
        [Route("uservalid")]
        public response uservalid(validation o)
        {
            return BLC.BLC.IsValidUser(o.email, o.password, o.data);
        }
        [HttpPost]
        [Route("register")]
        public void register(User use)
        {    
            BLC.BLC.Register(use);
       
        }
        [HttpPost]
        [Route("update")]
        public void update(User use)
        {
            DALC.DALC.update(use);

        }
        [HttpPost]
        [Route("acceptrequest")]
        public void accReq(Request request)
        {
            DALC.DALC.accrequest(request.f,request.t);

        }
        [HttpPost]
        [Route("sendrequest")]
        public void sendRequest(Request request)
        {
            DALC.DALC.sendrequest(request.f, request.t );


        }
        [HttpPost]
        [Route("addEvent")]
        public void addEvent(schedule meet)
        {
            DALC.DALC.addEvents(meet);


        }
        [HttpPost]
        [Route("removerequest")]
        public void removeRequest(Request request)
        {
            DALC.DALC.removerequest(request.f, request.t);


        }

    }
}
