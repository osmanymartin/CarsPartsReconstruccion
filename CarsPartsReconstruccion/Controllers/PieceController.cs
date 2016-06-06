﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarsPartsReconstruccion.Models;

namespace CarsPartsReconstruccion.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    public class PieceController : Controller
    {
        private db_cars_parts_reconstructionStrConn db = new db_cars_parts_reconstructionStrConn();

        //
        // GET: /Piece/
        public ActionResult Index()
        {
            return View(db.Pieces.ToList());
        }

        //
        // GET: /Piece/Details/5

        public ActionResult Details(int id = 0)
        {
            Piece piece = db.Pieces.Find(id);
            if (piece == null)
            {
                return HttpNotFound();
            }
            return View(piece);
        }

        //
        // GET: /Piece/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Piece/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Piece piece)
        {
            if (ModelState.IsValid)
            {
                db.Pieces.Add(piece);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(piece);
        }

        //
        // GET: /Piece/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Piece piece = db.Pieces.Find(id);
            if (piece == null)
            {
                return HttpNotFound();
            }
            return View(piece);
        }

        //
        // POST: /Piece/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Piece piece)
        {
            if (ModelState.IsValid)
            {
                db.Entry(piece).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(piece);
        }

        //
        // GET: /Piece/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id = 0)
        {
            Piece piece = db.Pieces.Find(id);
            if (piece == null)
            {
                return HttpNotFound();
            }
            return View(piece);
        }

        //
        // POST: /Piece/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Piece piece = db.Pieces.Find(id);
            db.Pieces.Remove(piece);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}